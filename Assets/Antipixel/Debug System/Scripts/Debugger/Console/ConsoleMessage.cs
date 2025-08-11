using UnityEngine;

namespace Antipixel.DebugSystem
{
	internal class ConsoleMessage
	{
		#region Fields
		private readonly string _message;
		private readonly LogType _type;
		private readonly ConsoleLogDate _date;
		#endregion Fields


		#region Constructors
		public ConsoleMessage(string message, LogType type, ConsoleLogDate date)
		{
			_message = message;
			_type = type;
			_date = date;
		}
		#endregion Constructors


		#region Properties
		public string Message => _message;
		public LogType Type => _type;
		public ConsoleLogDate Date => _date;
		#endregion Properties


		#region Methods
		public override string ToString() => $"{_date} {_message}";
		#endregion Methods
	}
}