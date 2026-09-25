using Aniki.Common.Reactive;
using Aniki.Parts;
using System.Collections.Generic;

namespace Aniki.Inventory {
	public interface IInventory {
		public void	Add(IEnumerable<PartSO> entity);
		public void	Remove(PartSO entity, int amount);
		public void	Open();
		public void	Close();
		public void	Clear();

		public IObservable<PartSO>	PartPutInto { get; }
		public IObservable<PartSO>	PartTakenOut { get; }
	}
}
