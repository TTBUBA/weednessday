namespace Antipixel.DebugSystem
{
	public class FPSText : ParameterText<FPSMonitor, FPSParameter>
	{
		#region Methods
		protected internal override object GetValue()
		{
			object value = base.GetValue();
			return value is ushort @ushort ? @ushort : (float)value;
		}
		#endregion Methods
	}
}