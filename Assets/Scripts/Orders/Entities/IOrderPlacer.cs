namespace Aniki.Orders {
	public interface IOrderPlacer {
		public void	Place(Order order);
		public void	Done(Order order);
		public void	Cancel(Order order);
	}
}
