using System;
using System.Collections;
using System.Collections.Generic;

namespace Aniki.Common.Reactive {
	public class ReactiveList<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, IList {
		private readonly	List<T>	list;

		private readonly ObservableEvent<ChangeEvent<T>>	changed = new();
		private readonly ObservableEvent<ElementEvent<T>>	added = new();
		private readonly ObservableEvent<ElementEvent<T>>	removed = new();
		private readonly ObservableEvent					cleared = new();

		public IObservable<ChangeEvent<T>>	Changed => changed;
		public IObservable<ElementEvent<T>>	Added => added;
		public IObservable<ElementEvent<T>>	Removed => removed;
		public IObservable					Cleared => cleared;

		public T	this[int index] { get => list[index]; set {
				T	old = list[index];
				list[index] = value;
				changed.Trigger(new(ReactiveEventType.REPLACE, index, old, value));
				removed.Trigger(new(index, old));
				added.Trigger(new(index, value));
			}
		}

		public int	Count => list.Count;

		public bool IsSynchronized => (list as ICollection).IsSynchronized;

		public object SyncRoot => (list as ICollection).SyncRoot;

		public bool	IsFixedSize => (list as IList).IsFixedSize;

		public bool	IsReadOnly => (list as IList).IsReadOnly;

		object IList.this[int index] { get => this[index]; set => this[index] = (T)value; }

		public void	Add(T item) {
			list.Add(item);
			changed.Trigger(new(ReactiveEventType.ADD, list.Count - 1, default, item));
			added.Trigger(new(list.Count - 1, item));
		}

		public void	Clear() {
			list.Clear();
			changed.Trigger(new(ReactiveEventType.CLEAR, 0, default, default));
			cleared.Trigger();
		}

		public bool	Contains(T item) {
			return list.Contains(item);
		}

		public void	CopyTo(T[] array, int arrayIndex) {
			list.CopyTo(array, arrayIndex);
		}

		public IEnumerator<T>	GetEnumerator() {
			return list.GetEnumerator();
		}

		public int	IndexOf(T item) {
			return list.IndexOf(item);
		}

		public void	Insert(int index, T item) {
			list.Insert(index, item);
			changed.Trigger(new(ReactiveEventType.ADD, index, default, item));
			added.Trigger(new(index, item));
		}

		public bool	Remove(T item) {
			int	i = list.IndexOf(item);
			
			if (i > -1)
				list.RemoveAt(i);
			return i > -1;
		}

		public void	RemoveAt(int index) {
			T	oldValue = list[index];
			list.RemoveAt(index);
			changed.Trigger(new(ReactiveEventType.REMOVE, index, oldValue, default));
			removed.Trigger(new(index, oldValue));
		}

		IEnumerator	IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public void	CopyTo(Array array, int index) {
			list.CopyTo((T[])array, index);
		}

		public int	Add(object value) {
			throw new NotSupportedException();
		}

		public bool	Contains(object value) {
			return Contains((T)value);
		}

		public int	IndexOf(object value) {
			return IndexOf((T)value);
		}

		public void	Insert(int index, object value) {
			Insert(index, (T)value);
		}

		public void	Remove(object value) {
			Remove((T)value);
		}

		public ReactiveList() {
			list = new();
		}

		public ReactiveList(IEnumerable<T> collection) {
			list = new(collection);
		}

		public ReactiveList(int capacity) {
			list = new(capacity);
		}
	}
}
