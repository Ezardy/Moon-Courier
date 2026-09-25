using Aniki.Common.Reactive;
using Aniki.Parts;
using System.Collections.Generic;
using UnInventory.Core.Manager;
using UnInventory.Core.MVC.Model.Data;
using UnInventory.Standard;
using UnInventory.Standard.MVC.Model.Commands.Primary.Create;
using UnInventory.Standard.MVC.Model.Commands.Primary.Move;
using UnInventory.Standard.MVC.Model.Commands.Primary.Remove;
using UnInventory.Standard.MVC.Model.Commands.Primary.SwapPrimary;
using UnInventory.Standard.MVC.Model.Listeners;
using UnityEngine;

namespace Aniki.Inventory {
	internal class Inventory : InventoryListener, IInventory {
		private readonly InventoryOpenCloseObject	inventory;
		private readonly CreateCommand				createCommand;
		private readonly RemoveCommand				removeCommand;

		private readonly ObservableEvent<PartSO>		putIntoEvent = new();
		private readonly ObservableEvent<PartSO>		takenOutEvent = new();

		public Inventory(GameObject warehouseInventoryPrefab) {
			inventory = new(warehouseInventoryPrefab, nameInventory: "WarehouseInventory");
			createCommand = InventoryManager.ContainerDi.Commands.Create<CreateCommand>();
			removeCommand = InventoryManager.ContainerDi.Commands.Create<RemoveCommand>();
			On();
		}

		public void	Add(IEnumerable<PartSO> entities) {
			bool	isClosed = !inventory.IsOpen;

			if (isClosed)
				Open();
			foreach (PartSO entity in entities) {
				IEnumerator<DataSlot>	slotEnum = InventoryManager.ContainerDi.DatabaseReadOnly.SlotsFree(inventory.DataInventory).GetEnumerator();
				bool					executed = false;
				while(!executed && slotEnum.MoveNext()) {
					if (!InventoryManager.ContainerDi.DatabaseReadOnly.IsOutBorderInventory(inventory.DataInventory, entity, slotEnum.Current.Vector2Int)) {
						createCommand.EnterData(new CreateInputData(entity, slotEnum.Current));
						executed = createCommand.ExecuteTry();
					}
				}
			}
			if (isClosed)
				Close();
		}

		public void	Clear() {
			foreach (DataEntity entity in InventoryManager.ContainerDi.DatabaseReadOnly.GetDataEntitiesInventory(inventory.DataInventory)) {
				removeCommand.EnterData(new RemoveInputData(entity, entity.Amount));
				if (removeCommand.ExecuteTry())
					Object.Destroy(entity);
			}
		}

		public void	Close() {
			inventory.Close();
		}

		public void	Open() {
			inventory.Open();
		}

		public void	Remove(PartSO entity, int amount) {
			removeCommand.EnterData(new RemoveInputData(entity, amount));
			if (removeCommand.ExecuteTry())
				Object.Destroy(entity);
		}

		protected override void	MoveReact(MoveDataAfterExecute data) {
			DataInventory	toInventory = data.InputData.ToInventory;
			DataInventory	fromInventory = data.InputData.FromInventory;
			DataInventory	inventory = this.inventory.DataInventory;

			if (!toInventory.Equals(fromInventory)) {
				if (ReferenceEquals(inventory, toInventory))
					putIntoEvent.Trigger((PartSO)data.EntityInNewPosition);
				else if (ReferenceEquals(inventory, fromInventory))
					takenOutEvent.Trigger((PartSO)data.EntityInNewPosition);
			}
		}

		protected override void SwapReact(SwapPrimaryDataAfterExecute data) {
			base.SwapReact(data);
			Debug.LogFormat("{0} {1}", data.InputData.EntitySource, data.InputData.EntityTarget);
		}

		public IObservable<PartSO>	PartPutInto => putIntoEvent;
		public IObservable<PartSO>	PartTakenOut => takenOutEvent;
	}
}
