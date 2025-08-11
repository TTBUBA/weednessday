using UnityEngine;

namespace Antipixel.DebugSystem
{
	public class RAMText : ParameterText<RAMMonitor, RAMParameter>
	{
		#region Methods
		protected internal override object GetValue() => (float)Mathf.RoundToInt((float)base.GetValue());
		#endregion Methods
	}
}