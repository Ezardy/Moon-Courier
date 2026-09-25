using System;
using UnInventory.Core.MVC.Model.Data;
using UnityEngine;

namespace Aniki.Parts {
	[CreateAssetMenu(fileName = "Part", menuName = "SO/Part")]
	public class PartSO : DataEntity, IEquatable<PartSO> {
		[SerializeField] private int	award;

		public int	Award => award;
		public int	Weight => Dimensions.x * Dimensions.y;

		public bool	Equals(PartSO other) {
			return Sprite == other.Sprite;
		}
	}
}
