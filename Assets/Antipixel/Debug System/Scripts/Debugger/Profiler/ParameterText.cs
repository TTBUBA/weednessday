using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Antipixel.DebugSystem
{
	public abstract class ParameterText<T1, T2> : MonoBehaviour
	{
		#region Fields
		[SerializeField] protected T2 _parameter;
		[SerializeField] protected T1 _monitor;
		[SerializeField] private string _format = "{0}";
		[SerializeField, Range(0f, 60f)] private float _updateRate = 1f;
		[SerializeField] private float _minValue;
		[SerializeField] private float _maxValue = 60f;
		[SerializeField] private Gradient _gradient;
		[SerializeField] private Threshold[] _thresholds;

		private Text _text;
		private TMP_Text _tmpText;
		private Threshold _previousThreshold;
		private float _updateTime;
		#endregion Fields


		#region Properties
		public T2 Parameter => _parameter;
		public float Value { get; private set; }
		public Threshold CurrentThreshold { get; private set; }
		#endregion Properties


		#region Unity Methods
		private void Awake()
		{
			_text = GetComponent<Text>();
			_tmpText = GetComponent<TMP_Text>();

			Array.Sort(_thresholds, (x, y) => y.Value.CompareTo(x.Value));
		}

		private void Update()
		{
			if (_monitor == null) return;

			Value = (float)GetValue();

			if ((_updateTime += Time.unscaledDeltaTime) > 1f / _updateRate)
			{
				_updateTime = 0f;

				float t = Mathf.InverseLerp(_minValue, _maxValue, Value);

				SetText(Value);
				SetColor(_gradient.Evaluate(t));
			}

			CurrentThreshold = GetCurrentThreshold();
			if (CurrentThreshold != null && CurrentThreshold != _previousThreshold)
			{
				_previousThreshold = CurrentThreshold;
				CurrentThreshold.Invoke();
			}
		}
		#endregion Unity Methods


		#region Methods
		public Threshold GetThreshold(string thresholdName) =>
			Array.Find(_thresholds, threshold => threshold.Name == thresholdName);

		protected internal virtual object GetValue() =>
			_monitor.GetType().GetProperty(_parameter.ToString()).GetValue(_monitor);

		private Threshold GetCurrentThreshold()
		{
			for (int i = 0; i < _thresholds.Length; i++)
			{
				Threshold threshold = _thresholds[i];

				if (i == 0 && Value >= threshold.Value)
					return threshold;
				else if (i == _thresholds.Length - 1 && Value <= threshold.Value)
					return threshold;
				else if (i - 1 >= 0)
				{
					Threshold previousThreshold = _thresholds[i - 1];

					if (Value > threshold.Value && Value < previousThreshold.Value)
						return threshold;
				}
			}

			return null;
		}

		private void SetText(object value)
		{
			string msg = string.Format(_format, value);

			if (_text != null) _text.text = msg;
			else if (_tmpText != null) _tmpText.text = msg;
		}
		private void SetColor(Color color)
		{
			if (_text != null) _text.color = color;
			else if (_tmpText != null) _tmpText.color = color;
		}
		#endregion Methods
	}
}