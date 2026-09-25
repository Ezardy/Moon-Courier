using UnityEngine;

namespace Aniki.Common.Reactive {
	[CreateAssetMenu(fileName = "ChannelSubscriber", menuName = "Channels/Local/Subscribers/Subscriber")]
	public class ChannelSubscriberSO : APooledDepSO<IChannelSubscriber> {
		[SerializeField] private Dep<IChannel>	channel;

		protected override IChannelSubscriber	Create(GameObject container) {
			return channel.Get(container);
		}
	}

	public abstract class AChannelSubscriberSO<T> : APooledDepSO<IChannelSubscriber<T>> {
		[SerializeField] private Dep<IChannel<T>>	channel;

		protected override IChannelSubscriber<T>	Create(GameObject container) {
			return channel.Get(container);
		}
	}
}
