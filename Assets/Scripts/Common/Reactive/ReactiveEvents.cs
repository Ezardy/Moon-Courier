namespace Aniki.Common.Reactive {
	public enum ReactiveEventType : byte {
		ADD,
		REMOVE,
		REPLACE,
		CLEAR
	}

	public readonly struct HashChangeEvent<T> {
		public readonly ReactiveEventType	Type;
		public readonly T					Value;

		public HashChangeEvent(ReactiveEventType type, T value) {
			Type = type;
			Value = value;
		}
	}

	public readonly struct ChangeEvent<T> {
		public readonly ReactiveEventType	Type;
		public readonly int					Index;
		public readonly T					OldValue;
		public readonly T					NewValue;

		public ChangeEvent(ReactiveEventType type, int index, T oldValue, T newValue) {
			Type = type;
			Index = index;
			OldValue = oldValue;
			NewValue = newValue;
		}
	}

	public readonly struct ElementEvent<T> {
		public readonly int	Index;
		public readonly T	Value;

		public ElementEvent(int index, T value) {
			Index = index;
			Value = value;
		}
	}
}
