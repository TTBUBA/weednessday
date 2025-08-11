using UnityEngine;
using UnityEngine.Profiling;

namespace Antipixel.DebugSystem
{
    public class RAMMonitor : MonoBehaviour
    {
		#region Properties
		public float Reserved { get; private set; }
		public float Allocated { get; private set; }
		public float Mono { get; private set; }
		#endregion Properties


		#region Unity Methods
		private void Update()
		{
			Reserved = Profiler.GetTotalReservedMemoryLong() / 1048576f;
			Allocated = Profiler.GetTotalAllocatedMemoryLong() / 1048576f;
			Mono = Profiler.GetMonoUsedSizeLong() / 1048576f;
		}
		#endregion Unity Methods
	}
}