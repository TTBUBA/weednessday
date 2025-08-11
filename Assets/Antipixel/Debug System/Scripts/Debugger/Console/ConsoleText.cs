using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Antipixel.DebugSystem
{
	public class ConsoleText : MonoBehaviour
	{
		#region Fields
		[SerializeField] private ConsoleLogFlag types;
		[SerializeField] private ConsoleMonitor monitor;

		private Text _text;
		private TMP_Text _tmpText;
		private string _search;
		private int _index;
		#endregion Fields


		#region Unity Methods
		private void OnEnable()
		{
			monitor.OnConsoleLogMessage += OnLogMessage;

			Refresh();
		}
		private void OnDisable() => monitor.OnConsoleLogMessage -= OnLogMessage;

		private void Awake()
		{
			_text = GetComponent<Text>();
			_tmpText = GetComponent<TMP_Text>();
		}
		#endregion Unity Methods


		#region Methods
		public void SetLog(bool value) => SetFlag(ConsoleLogFlag.Log, value);
		public void SetWarning(bool value) => SetFlag(ConsoleLogFlag.Warning, value);
		public void SetError(bool value) => SetFlag(ConsoleLogFlag.Error, value);
		private void SetFlag(ConsoleLogFlag flag, bool value)
		{
			if (value) types |= flag;
			else types &= ~flag;

			Refresh();
		}

		public void Search(string search)
		{
			_search = search;

			Refresh();
		}

		public void Clear()
		{
			ClearText();

			_index = monitor.history.Count;
		}
		private void ClearText()
		{
			if (_text != null) _text.text = string.Empty;
			else if (_tmpText != null) _tmpText.text = string.Empty;
		}

		private void OnLogMessage(ConsoleMessage message) => LogMessage(message);
		private void LogMessage(ConsoleMessage message)
		{
			if ((message.Type == LogType.Log && !HasFlag(ConsoleLogFlag.Log)) ||
				(message.Type == LogType.Warning && !HasFlag(ConsoleLogFlag.Warning)) ||
				(message.Type != LogType.Log && message.Type != LogType.Warning && !HasFlag(ConsoleLogFlag.Error)))
				return;

			if (!string.IsNullOrEmpty(_search) && !message.Message.ToLower().Contains(_search.ToLower()))
				return;

			string msg = message + "\n";

			if (_text != null) _text.text += msg;
			else if (_tmpText != null) _tmpText.text += msg;

			bool HasFlag(ConsoleLogFlag flag) => (types & flag) == flag;
		}

		private void Refresh()
		{
			ClearText();

			for (int i = _index; i < monitor.history.Count; i++)
				LogMessage(monitor.history[i]);
		}
		#endregion Methods
	}
}