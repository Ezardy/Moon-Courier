using System;

namespace Aniki.Common.Reactive {
	public class Observable<T> : IObservable<T> {
		protected Action<T>	triggered;

		public IDisposable	Subscribe(Action<T> action) {
			triggered += action;
			return new Unsubscriber(this, action);
		}

		private void	Unsubscribe(Action<T> action) {
			triggered -= action;
		}

		private class Unsubscriber : IDisposable {
			private readonly Observable<T>	observable;
			private readonly Action<T>		action;

			public Unsubscriber(Observable<T> observable, Action<T> action) {
				this.observable = observable;
				this.action = action;
			}

			public void	Dispose() {
				observable.Unsubscribe(action);
			}
		}
	}

	public class Observable : IObservable {
		protected Action	triggered;

		public IDisposable	Subscribe(Action action) {
			triggered += action;
			return new Observer(this, action);
		}

		private void	Unsubscribe(Action action) {
			triggered -= action;
		}

		private class Observer : IDisposable {
			private readonly Observable	observable;
			private readonly Action		action;

			public Observer(Observable observable, Action action) {
				this.observable = observable;
				this.action = action;
			}

			public void	Dispose() {
				observable.Unsubscribe(action);
			}
		}
	}
}
