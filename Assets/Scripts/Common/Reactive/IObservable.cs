using System;

namespace Aniki.Common.Reactive {
	public interface IObservable<T> {
		public IDisposable	Subscribe(Action<T> action);
	}

	public interface IObservable {
		public IDisposable	Subscribe(Action action);
	}
}
