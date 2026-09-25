using Aniki.Common;
using System.Collections.Generic;
using UnInventory.Core.MVC.Model.Data;
using UnityEngine;
using Aniki.Parts;
using System.Linq;

namespace Aniki.Inventory {
	internal class PartsSpawnInventoryTest : MonoBehaviour {
		[SerializeField] private Dep<IInventory>	inventory;

		private void	Start() {
			IInventory				inventory = this.inventory.Get();
			IEnumerable<DataEntity>	entities = UnInventory.Core.Extensions.ResourcesExt.LoadDataEntities("Parts");
			inventory.Add(entities.Cast<PartSO>());
		}
	}
}
