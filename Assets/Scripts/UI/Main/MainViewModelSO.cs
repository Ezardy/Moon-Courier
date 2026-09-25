using Aniki.Common;
using System;
using Unity.Properties;
using UnityEngine;

namespace Aniki.UI {
	[CreateAssetMenu(fileName = "MainViewModel", menuName = "SO/View Models/Main")]
	internal class MainViewModelSO : ASingleDepSO<MainViewModel> {
		[SerializeField] private Dep<MainView>	view;

		[Header("Publishers")]

		[SerializeField] private Ref<IChannelPublisher>	mapClickedChannelPublisher;
		[SerializeField] private Ref<IChannelPublisher>	inventoryCloseClickedChannelPublisher;
		[SerializeField] private Ref<IChannelPublisher>	mapCloseClickedChannelPublisher;

		[Header("Subscriber")]
		[SerializeField] private Ref<IChannelSubscriber>	cargoClickedChannelSubscriber;
		[SerializeField] private Ref<IChannelSubscriber>	mapOpenAnimationEndedSubscriber;
		[SerializeField] private Ref<IChannelSubscriber>	mapCloseAnimationEndedSubscriber;

		public IChannelPublisher	MapClickedPublisher => mapClickedChannelPublisher.I;
		public IChannelPublisher	InventoryCloseClickedPublisher => inventoryCloseClickedChannelPublisher.I;
		public IChannelPublisher	MapCloseClickedPublisher => mapCloseClickedChannelPublisher.I;
		public IChannelSubscriber	CargoClickedSubscriber => cargoClickedChannelSubscriber.I;
		public IChannelSubscriber	MapOpenAnimationEndedSubscriber => mapOpenAnimationEndedSubscriber.I;
		public IChannelSubscriber	MapCloseAnimationEndedSubscriber => mapCloseAnimationEndedSubscriber.I;

		protected override MainViewModel	Create(GameObject container) {
			return new(this, view.Get(container));
		}
	}

	internal class MainViewModel : IDisposable {
		private readonly MainViewModelSO	sourceSO;
		private readonly MainView			view;
		private readonly IDisposable		disposable;

		[CreateProperty] public bool	IsBaseScreenShown { get; private set; } = true;
		[CreateProperty] public bool	IsInventoryScreenShown { get; private set; }
		[CreateProperty] public bool	IsMapScreenShown { get; private set; }

		public MainViewModel(MainViewModelSO sourceSO, MainView view) {
			IDisposable	d1 = sourceSO.CargoClickedSubscriber.Subscribe(OnCargoClicked);
			IDisposable	d2 = sourceSO.MapOpenAnimationEndedSubscriber.Subscribe(OnMapOpenAnimationEnded);
			IDisposable	d3 = sourceSO.MapCloseAnimationEndedSubscriber.Subscribe(OnMapCloseAnimationEnded);

			disposable = new Disposable(d1, d2, d3);
			this.sourceSO = sourceSO;
			this.view = view;
			Init();
		}

		private async void	Init() {
			await AwaitableExtensions.WaitUntil(() => view.IsReady);

			view.Root.dataSource = this;
			view.Map.clicked += OnMapClicked;
			view.CloseInventory.clicked += OnInventoryCloseClicked;
			view.CloseMap.clicked += OnMapCloseClicked;
		}

		private void	OnMapClicked() {
			sourceSO.MapClickedPublisher.Publish();

			IsBaseScreenShown = false;
		}

		private void	OnInventoryCloseClicked() {
			sourceSO.InventoryCloseClickedPublisher.Publish();

			IsInventoryScreenShown = false;
			IsBaseScreenShown = true;
		}

		private void	OnMapCloseClicked() {
			sourceSO.MapCloseClickedPublisher.Publish();

			IsMapScreenShown = false;
		}

		private void	OnCargoClicked() {
			IsInventoryScreenShown = true;
			IsBaseScreenShown = false;
		}

		private void	OnMapOpenAnimationEnded() {
			IsMapScreenShown = true;
		}

		private void	OnMapCloseAnimationEnded() {
			IsBaseScreenShown = true;
		}

		public void	Dispose() {
			view.CloseMap.clicked -= OnMapCloseClicked;
			view.CloseInventory.clicked -= OnInventoryCloseClicked;
			view.Map.clicked -= OnMapClicked;
			disposable.Dispose();
		}
	}
}
