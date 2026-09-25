using System;

namespace Aniki.Common {
	public class Disposable : IDisposable {
		private readonly IDisposable[]	disposables;

		public Disposable(params IDisposable[] disposables) {
			this.disposables = disposables;
		}

		public void	Dispose() {
			foreach (IDisposable d in disposables)
				d.Dispose();
		}
	}
}
