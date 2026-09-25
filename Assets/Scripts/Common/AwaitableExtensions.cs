using System;
using UnityEngine;

namespace Aniki.Common {
	public static class AwaitableExtensions {
		public static async Awaitable	WaitUntil(Func<bool> predicate) {
			while (!predicate())
				await Awaitable.NextFrameAsync();
		}

		public static async Awaitable	WaitWhile(Func<bool> predicate) {
			while (predicate())
				await Awaitable.NextFrameAsync();
		}
	}
}
