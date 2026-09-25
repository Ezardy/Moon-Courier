using Aniki.Common;
using UnityEngine;

namespace Aniki.Inventory {
	[CreateAssetMenu(fileName = "GlobalInventory", menuName = "SO/Global Inventory")]
	internal class GlobalInventorySO : ASingleDepSO<IInventory> {
		[SerializeField] private GameObject	inventoryPrefab;

		protected override IInventory	Create(GameObject container) {
			return new Inventory(inventoryPrefab);
		}
	}
}
