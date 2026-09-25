using Aniki.Common;
using UnityEngine;

namespace Aniki.Inventory {
	public class InventoryInstaller : MonoBehaviour {
		[SerializeField] private Dep<IInventory>	inventory;

		public IInventory	Inventory { get; private set; }

		private void	Awake() {
			Inventory = inventory.Get(gameObject);
		}
	}
}
