using Aniki.Common;
using UnityEngine;

namespace Aniki.Orders {
	internal class OrderPlacerSO : ASingleDepSO<IOrderPlacer> {
		[SerializeField] private Dep<IOrderListModel>	orderListModel;

		protected override IOrderPlacer	Create(GameObject container) {
			return new OrderPlacer(orderListModel.Get(container));
		}
	}

	internal class OrderPlacer : IOrderPlacer {
		protected readonly IOrderListModel	orderListModel;

		public OrderPlacer(IOrderListModel orderListModel) {
			this.orderListModel = orderListModel;
		}

		public void	Cancel(Order order) {
			order.Status = OrderStatus.CANCELED;
			orderListModel.Remove(order);
		}

		public void	Done(Order order) {
			order.Status = OrderStatus.DONE;
			orderListModel.Remove(order);
		}

		public void	Place(Order order) {
			order.Status = OrderStatus.PENDING;
			orderListModel.Place(order);
		}
	}
}
