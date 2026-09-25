using Aniki.Common;
using Aniki.Common.Reactive;
using Aniki.Parts;
using Aniki.Path;
using System;
using System.Linq;

namespace Aniki.Rover {
	public class RoverModel : IDisposable {
		public const float	weightConsumption = 0.2f;

		private readonly RoverModelSO	sourceSO;
		private readonly IDisposable	disposable;

		public MissionType				Type { get; set; }
		public PathModel				Path { get; set; }
		public float					Battery { get; set; }
		public ReactiveHashSet<PartSO>	Cargo {
			get => cargo;
			set {
				cargo = value;
				weight = cargo.Sum(p => p.Weight);
			}
		}
		public float					PathProgress { get; set; }

		private int						weight;
		private ReactiveHashSet<PartSO>	cargo = new();

		public float	BatteryCapacity => sourceSO.BatteryCapacity;
		public float	Consumption => sourceSO.Consumption + weight * weightConsumption;
		public int		Weight => weight;
		public int		WeightCapacity => sourceSO.WeightCapacity;
		public int		PowerForPath => Path == null ? 0 : (int)MathF.Ceiling(Consumption * Path.Distance);

		internal RoverModel(RoverModelSO sourceSO) {
			this.sourceSO = sourceSO;
			Battery = sourceSO.BatteryCapacity;

			IDisposable	addDisposable = cargo.Added.Subscribe(p => weight += p.Weight);
			IDisposable	removeDisposable = cargo.Removed.Subscribe(p => weight -= p.Weight);
			IDisposable	clearDisposable = cargo.Cleared.Subscribe(() => weight = 0);

			disposable = new Disposable(addDisposable, removeDisposable, clearDisposable);
		}

		public void	Dispose() {
			disposable?.Dispose();
		}
	}

	public enum MissionType : byte {
		NONE,
		DELIVERY,
		RETURN
	}
}
