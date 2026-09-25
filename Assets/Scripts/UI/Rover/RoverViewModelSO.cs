using Aniki.Common;
using Aniki.Inventory;
using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aniki.Rover {
	[CreateAssetMenu(fileName = "RoverViewModel", menuName = "SO/View Models/Rover")]
	internal class RoverViewModelSO : APooledDepSO<RoverViewModel> {
		[SerializeField] private Dep<RoverView>		roverView;
		[SerializeField] private Dep<IInventory>	warehouseInventory;

		[Header("Channel Publishers")]

		[SerializeField] private Ref<IChannelPublisher>	cargoClickedGlobalPublisher;
		[SerializeField] private Dep<IChannelPublisher>	sendClickedLocalPublisher;

		[Header("Channel Subscribers")]

		[SerializeField] private Ref<IChannelSubscriber>	closeInventoryClickedSubscriber;

		public IInventory			WarehouseInventory => warehouseInventory.Get();
		public IChannelPublisher	CargoClickedPublisher => cargoClickedGlobalPublisher.I;
		public IChannelSubscriber	CloseInventoryClickedSubscriber => closeInventoryClickedSubscriber.I;

		protected override RoverViewModel	Create(GameObject container) {
			return new(this, container.GetComponent<RoverInstaller>().Model,
				roverView.Get(container),
				container.GetComponent<InventoryInstaller>().Inventory,
				sendClickedLocalPublisher.Get(container));
		}

	#if UNITY_EDITOR
		[InitializeOnLoadMethod]
	#else
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	#endif
		private static void	RegisterConverters() {
			ConverterGroup	f1PerKmGroup = new("F1 per km");

			f1PerKmGroup.AddConverter((ref float value) => {
				string	cons = RoundConverters.F1Converter(ref value);
				return SuffixConverters.PerKmConverter(ref cons);
			});
			ConverterGroups.RegisterConverterGroup(f1PerKmGroup);
		}
	}

	internal class RoverViewModel : IDisposable {
		private readonly RoverViewModelSO	sourceSO;
		private readonly RoverModel			model;
		private readonly RoverView			view;
		private readonly IInventory			roverInventory;
		private readonly IChannelPublisher	sendClickedPublisher;
		private readonly IDisposable		disposable;

		[CreateProperty] public bool	IsShown { get; private set; } = true;
		[CreateProperty] public float	BatteryCapacity => model.BatteryCapacity;
		[CreateProperty] public float	WeightCapacity => model.WeightCapacity;

		[CreateProperty] public float	Battery => model.Battery;
		[CreateProperty] public int		Weight => model.Weight;
		[CreateProperty] public int		SendPrice => model.PowerForPath;
		[CreateProperty] public float	Consumption => model.Consumption;

		private IDisposable	closeInventorySubscription;

		public RoverViewModel(RoverViewModelSO sourceSO,
			RoverModel model, RoverView view, IInventory roverInventory,
			IChannelPublisher sendClickedPublisher) {
			IDisposable	d1 = roverInventory.PartPutInto.Subscribe(p => model.Cargo.Add(p));
			IDisposable	d2 = roverInventory.PartTakenOut.Subscribe(p => model.Cargo.Remove(p));

			disposable = new Disposable(d1, d2);
			this.sourceSO = sourceSO;
			this.model = model;
			this.view = view;
			this.roverInventory = roverInventory;
			this.sendClickedPublisher = sendClickedPublisher;
			Init();
		}

		private async void	Init() {
			await AwaitableExtensions.WaitWhile(() => view.Root == null);

			view.Root.dataSource = this;
			view.Cargo.clicked += CargoClicked;
			view.Send.clicked += SendClicked;
		}

		private void	CargoClicked() {
			IsShown = false;
			sourceSO.CargoClickedPublisher.Publish();
			sourceSO.WarehouseInventory.Open();
			roverInventory.Open();
			closeInventorySubscription = sourceSO.CloseInventoryClickedSubscriber.Subscribe(CloseInventoryClicked);
		}

		private void	SendClicked() {
			sendClickedPublisher.Publish();
		}

		private void	CloseInventoryClicked() {
			IsShown = true;
			closeInventorySubscription.Dispose();
			closeInventorySubscription = null;
			sourceSO.WarehouseInventory.Close();
			roverInventory.Close();
		}

		public void	Dispose() {
			closeInventorySubscription?.Dispose();
			view.Send.clicked -= SendClicked;
			view.Cargo.clicked -= CargoClicked;
			disposable.Dispose();
		}
	}
}
