namespace Antipixel.DebugSystem
{
	internal struct ConsoleLogDate
	{
		#region Fields
		private readonly int _hour;
		private readonly int _minute;
		private readonly int _second;
		#endregion Fields


		#region Constructors
		public ConsoleLogDate(int hour, int minute, int second)
		{
			_hour = hour;
			_minute = minute;
			_second = second;
		}
		#endregion Constructors


		#region Properties
		public int Hour => _hour;
		public int Minute => _minute;
		public int Second => _second;
		#endregion Properties


		#region Methods
		public override string ToString() => string.Format("[{0:00}:{1:00}:{2:00}]", _hour, _minute, _second);
		#endregion Methods
	}
}