using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Antipixel.DebugSystem
{
	public class ConsoleMonitor : MonoBehaviour
	{
		#region Constants
		private const string EXTENSION = ".txt";
		#endregion Constants


		#region Fields
		internal List<ConsoleMessage> history = new List<ConsoleMessage>();
		#endregion Fields


		#region Events
		internal event Action<ConsoleMessage> OnConsoleLogMessage;
		#endregion Events


		#region Properties
		private int Month => DateTime.UtcNow.Month;
		private int Day => DateTime.UtcNow.Day;
		private int Year => DateTime.UtcNow.Year;
		private int Hour => DateTime.UtcNow.Hour;
		private int Minute => DateTime.UtcNow.Minute;
		private int Second => DateTime.UtcNow.Second;
		private int Millisecond => DateTime.UtcNow.Millisecond;
		#endregion Properties


		#region Unity Methods
		private void OnEnable() => Application.logMessageReceived += OnLogMessage;
		private void OnDisable() => Application.logMessageReceived -= OnLogMessage;
		#endregion Unity Methods


		#region Methods
		public void Log(string msg) => Debug.Log(msg);
		public void LogWarning(string msg) => Debug.LogWarning(msg);
		public void LogError(string msg) => Debug.LogError(msg);

		public void Save(string fileName = null)
		{
			if (string.IsNullOrEmpty(fileName))
				fileName = $"{Month}{Day}{Year}{Hour}{Minute}{Second}{Millisecond}{EXTENSION}";
			else if (!fileName.Contains(EXTENSION))
				fileName += EXTENSION;

			string path = Path.Combine(Application.persistentDataPath, fileName);

			if (File.Exists(path)) File.Delete(path);

			using StreamWriter writer = new StreamWriter(path, true);
			foreach (ConsoleMessage message in history)
				writer.WriteLine($"{message.Date} {message.Message}");

			Log($"The log file has been saved in <i>{Application.persistentDataPath}</i>");
		}

		private void OnLogMessage(string msg, string stackTrace, LogType logType)
		{
			ConsoleMessage message = new ConsoleMessage(msg, logType, new ConsoleLogDate(Hour, Minute, Second));

			history.Add(message);
			OnConsoleLogMessage?.Invoke(message);
		}
		#endregion Methods
	}
}