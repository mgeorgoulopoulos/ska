using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ska {
	class Helper {
		private class PerThreadData {
			public Random rnd;

			public PerThreadData() {
				// make sure all threads have different random seeds
				rnd = new Random(Thread.CurrentThread.ManagedThreadId);
			}
		}

		private static ThreadLocal<PerThreadData> threadLocalData = new ThreadLocal<PerThreadData>();

		private static PerThreadData perThreadData {
			get {
				if (!threadLocalData.IsValueCreated) threadLocalData.Value = new PerThreadData();
				return threadLocalData.Value;
			}
		}

		public static int RandomInt(int from, int to) {
			return perThreadData.rnd.Next(from, to);
		}



	}
}
