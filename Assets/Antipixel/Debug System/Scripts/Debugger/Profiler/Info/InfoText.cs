using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Antipixel.DebugSystem
{
	public class InfoText : MonoBehaviour
	{
		#region Fields
		[SerializeField] private InfoParameter[] _parameters;
		[SerializeField] private string _format = "{0}";
		[SerializeField, Range(0f, 60f)] private float _updateRate;

		private Text _text;
		private TMP_Text _tmpText;
		private float _updateTime;
		#endregion Fields


		#region Unity Methods
		private void Awake()
		{
			_text = GetComponent<Text>();
			_tmpText = GetComponent<TMP_Text>();

			Refresh();
		}

		private void Update()
		{
			if ((_updateTime += Time.unscaledDeltaTime) > 1f / _updateRate)
			{
				_updateTime = 0f;

				Refresh();
			}
		}
		#endregion Unity Methods


		#region Methods
		public void Refresh()
		{
			List<object> values = new List<object>();

			foreach (InfoParameter parameter in _parameters)
			{
				object value;

				switch (parameter)
				{
					case InfoParameter.ScreenHeight: value = Screen.currentResolution.height; break;
					case InfoParameter.ScreenRefreshRate: value = Screen.currentResolution.refreshRate; break;
					case InfoParameter.ScreenWidth: value = Screen.currentResolution.width; break;
					case InfoParameter.WindowDpi: value = Screen.dpi; break;
					case InfoParameter.WindowFullscreen: value = Screen.fullScreen; break;
					case InfoParameter.WindowHeight: value = Screen.height; break;
					case InfoParameter.WindowWidth: value = Screen.width; break;

					default:
						string propertyName = parameter.ToString();
						propertyName = char.ToLower(propertyName[0]) + propertyName[1..];
						value = typeof(SystemInfo).GetProperty(propertyName).GetValue(null);
						break;
				}

				values.Add(value);
			}

			SetText(values.ToArray());
		}

		private void SetText(params object[] values)
		{
			if (values == null || values.Length == 0) return;

			string msg = string.Format(_format, values);

			if (_text != null) _text.text = msg;
			else if (_tmpText != null) _tmpText.text = msg;
		}
		#endregion Methods
	}
}