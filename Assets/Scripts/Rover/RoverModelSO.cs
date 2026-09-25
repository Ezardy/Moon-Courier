using Aniki.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Aniki.Rover {
	[CreateAssetMenu(fileName = "RoverModel", menuName = "SO/Models/Rover")]
	internal class RoverModelSO : APooledDepSO<RoverModel> {
		[SerializeField, Min(0)] private float	batteryCapacity = 100;
		[SerializeField, Min(0)] private int	weightCapacity = 4;
		[SerializeField, Min(0)] private float	consumption = 5;

		public float	BatteryCapacity => batteryCapacity;
		public float	Consumption => consumption;
		public int		WeightCapacity => weightCapacity;

		protected override RoverModel	Create(GameObject container) {
			return new RoverModel(this);
		}
	}
}
