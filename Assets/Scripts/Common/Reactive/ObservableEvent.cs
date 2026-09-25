namespace Aniki.Common.Reactive {
	public class ObservableEvent<T> : Observable<T> {
		public void	Trigger(T value) {
			triggered?.Invoke(value);
		}
	}

	public class ObservableEvent : Observable {
		public void	Trigger() {
			triggered?.Invoke();
		}
	}
}
