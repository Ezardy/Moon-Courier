using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Aniki.Common.Reactive {
	public class ReactiveHashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ISet<T>, IDeserializationCallback, ISerializable {
		private readonly	HashSet<T>	set;

		private readonly ObservableEvent<HashChangeEvent<T>>	changed = new();
		private readonly ObservableEvent<T>						added = new();
		private readonly ObservableEvent<T>						removed = new();
		private readonly ObservableEvent						cleared = new();

		public IObservable<HashChangeEvent<T>>	Changed => changed;
		public IObservable<T>					Added => added;
		public IObservable<T>					Removed => removed;
		public IObservable						Cleared => cleared;

		public int	Count => set.Count;

		public bool	IsReadOnly => (set as ISet<T>).IsReadOnly;

		public void	Add(T item) {
			(this as ISet<T>).Add(item);
		}

		public void	Clear() {
			set.Clear();
			changed.Trigger(new(ReactiveEventType.CLEAR, default));
			cleared.Trigger();
		}

		public bool	Contains(T item) {
			return set.Contains(item);
		}

		public void	CopyTo(T[] array, int arrayIndex) {
			set.CopyTo(array, arrayIndex);
		}

		public void	ExceptWith(IEnumerable<T> other) {
			set.ExceptWith(other);
		}

		public IEnumerator<T>	GetEnumerator() {
			return set.GetEnumerator();
		}

		public void	GetObjectData(SerializationInfo info, StreamingContext context) {
			set.GetObjectData(info, context);
		}

		public void	IntersectWith(IEnumerable<T> other) {
			set.IntersectWith(other);
		}

		public bool	IsProperSubsetOf(IEnumerable<T> other) {
			return set.IsProperSubsetOf(other);
		}

		public bool	IsProperSupersetOf(IEnumerable<T> other) {
			return set.IsProperSupersetOf(other);
		}

		public bool	IsSubsetOf(IEnumerable<T> other) {
			return set.IsSubsetOf(other);
		}

		public bool	IsSupersetOf(IEnumerable<T> other) {
			return set.IsSupersetOf(other);
		}

		public void	OnDeserialization(object sender) {
			set.OnDeserialization(sender);
		}

		public bool	Overlaps(IEnumerable<T> other) {
			return set.Overlaps(other);
		}

		public bool	Remove(T item) {
			bool	removed = set.Remove(item);

			if (removed) {
				changed.Trigger(new(ReactiveEventType.REMOVE, item));
				this.removed.Trigger(item);
			}
			return removed;
		}

		public bool	SetEquals(IEnumerable<T> other) {
			return set.SetEquals(other);
		}

		public void	SymmetricExceptWith(IEnumerable<T> other) {
			set.SymmetricExceptWith(other);
		}

		public void	UnionWith(IEnumerable<T> other) {
			set.UnionWith(other);
		}

		bool	ISet<T>.Add(T item) {
			bool	added = set.Add(item);

			if (added) {
				changed.Trigger(new(ReactiveEventType.ADD, item));
				this.added.Trigger(item);
			}
			return added;
		}

		IEnumerator	IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public ReactiveHashSet() {
			set = new();
		}

		public ReactiveHashSet(int capacity) {
			set = new(capacity);
		}

		public ReactiveHashSet(IEnumerable<T> collection) {
			set = new(collection);
		}

		public ReactiveHashSet(IEqualityComparer<T> comparer) {
			set = new(comparer);
		}

		public ReactiveHashSet(int capacity, IEqualityComparer<T> comparer) {
			set = new(capacity, comparer);
		}

		public ReactiveHashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) {
			set = new(collection, comparer);
		}
	}
}
