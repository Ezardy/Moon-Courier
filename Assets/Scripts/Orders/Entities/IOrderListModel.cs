using System;

namespace Aniki.Orders {
	internal interface IOrderListModel {
		public void	Place(Order order);
		public void	Remove(Order order);

		public event Action<Order>	OrderPlaced;
		public event Action<Order>	OrderRemoved;
	}
}
