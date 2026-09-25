using Aniki.Common;
using Aniki.Parts;
using System.Collections.Generic;

namespace Aniki.Orders {
	public class Order {
		private OrderStatus							status;
		private readonly int						award;
		private readonly IReadOnlyCollection<PartSO>	parts;
		private readonly float						expirationTime;

		public Order(IReadOnlyCollection<PartSO> parts, float expirationTime, int award, OrderStatus status = OrderStatus.PENDING) {
			this.status = status;
			this.award = award;
			this.parts = parts;
			this.expirationTime = expirationTime;
		}

		public OrderStatus	Status { get => status; set => status = value; }

		public int	Award => award;

		public IReadOnlyCollection<PartSO>	Parts => parts;

		public float	ExpirationTime => expirationTime;
	}
}
