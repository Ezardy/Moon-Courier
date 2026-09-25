using UnityEngine;

namespace Aniki.Common {
	[CreateAssetMenu(fileName = "ChannelPublisher", menuName = "Channels/Local/Publishers/Publisher")]
	public class ChannelPublisherSO : APooledDepSO<IChannelPublisher> {
		[SerializeField] private Dep<IChannel>	channel;

		protected override IChannelPublisher	Create(GameObject container) {
			return channel.Get(container);
		}
	}

	public abstract class AChannelPublisherSO<T> : APooledDepSO<IChannelPublisher<T>> {
		[SerializeField] protected Dep<IChannel<T>>	channel;

		protected override IChannelPublisher<T>	Create(GameObject container) {
			return channel.Get(container);
		}
	}
}
