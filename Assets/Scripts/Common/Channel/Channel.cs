using Aniki.Common.Reactive;
using System;

namespace Aniki.Common {
	public class Channel : IChannel {
		private readonly ObservableEvent	observableEvent = new();

		public void	Publish() {
			observableEvent.Trigger();
		}

		public IDisposable	Subscribe(Action action) {
			return observableEvent.Subscribe(action);
		}
	}

	public class Channel<T> : IChannel<T> {
		private readonly ObservableEvent<T>	observableEvent = new();

		public void	Publish(T value) {
			observableEvent.Trigger(value);
		}

		public IDisposable	Subscribe(Action<T> action) {
			return observableEvent.Subscribe(action);
		}
	}
}
