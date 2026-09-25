using Aniki.Common.Reactive;

namespace Aniki.Common {
	public interface IChannelPublisher {
		public void	Publish();
	}

	public interface IChannelSubscriber : IObservable { }

	public interface IChannel : IChannelPublisher, IChannelSubscriber { }

	public interface IChannelPublisher<T> {
		public void	Publish(T value);
	}

	public interface IChannelSubscriber<T> : IObservable<T> { }

	public interface IChannel<T> : IChannelPublisher<T>, IChannelSubscriber<T> { }
}
