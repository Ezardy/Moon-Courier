using Aniki.Common;
using UnityEngine;

namespace Aniki.Inventory {
	[CreateAssetMenu(fileName = "Inventory", menuName = "SO/Inventory")]
	internal class InventorySO : APooledDepSO<IInventory> {
		[SerializeField] private GameObject	inventoryPrefab;

		protected override IInventory	Create(GameObject container) {
			return new Inventory(inventoryPrefab);
		}
	}
}
