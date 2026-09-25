using Aniki.Common.Reactive;
using System;
using UnityEngine;

namespace Aniki.Common {
	public abstract class AChannel<T> : ScriptableObject, IChannel<T> {
		private readonly ObservableEvent<T>	observableEvent = new();

		public void	Publish(T value) {
			observableEvent.Trigger(value);
		}

		public IDisposable	Subscribe(Action<T> action) {
			return observableEvent.Subscribe(action);
		}
	}
}
