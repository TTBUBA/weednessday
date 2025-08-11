using System;
using UnityEngine;

namespace Antipixel.DebugSystem
{
	public class FPSMonitor : MonoBehaviour
	{
		#region Constants
		private const ushort CAPACITY = 1024;
		#endregion Constants


		#region Fields
		private ushort[] _fpsSamples = new ushort[CAPACITY];
		private ushort[] _fpsSamplesSorted = new ushort[CAPACITY];
		private ushort _low1Samples = CAPACITY / 100;
		private ushort _low01Samples = CAPACITY / 1000;
		private ushort _fpsSamplesCount;
		private ushort _indexSample;
		private bool _canUpdateMinMax;
		#endregion Fields


		#region Properties
		public ushort Current { get; private set; }
		public ushort Average { get; private set; }
		public ushort Minimum { get; private set; } = ushort.MaxValue;
		public ushort Maximum { get; private set; }
		public ushort Low1 { get; private set; }
		public ushort Low01 { get; private set; }
		public float Latency => Time.unscaledDeltaTime * 1000f;
		#endregion Properties


		#region Unity Methods
		private void Start() => ResetValues();

		private void Update()
		{
			UpdateFPS();
			UpdateAverage();
			UpdatePercentLows();
		}
		#endregion Unity Methods


		#region Methods
		public void ResetValues()
		{
			_canUpdateMinMax = false;
			Invoke(nameof(StartUpdateMinMax), 0.1f);

			_fpsSamples = new ushort[CAPACITY];
			_fpsSamplesSorted = new ushort[CAPACITY];
			_low1Samples = 10;
			_low01Samples = 1;
			_fpsSamplesCount = 0;
			_indexSample = 0;

			Current = 0;
			Average = 0;
			Minimum = ushort.MaxValue;
			Maximum = 0;
			Low1 = 0;
			Low01 = 0;
		}

		private void UpdateFPS()
		{
			Current = (ushort)(Mathf.RoundToInt(1f / Time.unscaledDeltaTime));

			if (_canUpdateMinMax)
			{
				if (Current < Minimum) Minimum = Current;
				if (Current > Maximum) Maximum = Current;
			}
		}
		private void UpdateAverage()
		{
			uint average = 0;

			_indexSample++;

			if (_indexSample >= CAPACITY) _indexSample = 0;

			_fpsSamples[_indexSample] = Current;

			if (_fpsSamplesCount < CAPACITY)
				_fpsSamplesCount++;

			for (int i = 0; i < _fpsSamplesCount; i++)
				average += _fpsSamples[i];

			Average = (ushort)((float)average / _fpsSamplesCount);
		}
		private void UpdatePercentLows()
		{
			_fpsSamples.CopyTo(_fpsSamplesSorted, 0);

			Array.Sort(_fpsSamplesSorted, (x, y) => x.CompareTo(y));

			bool low01Calculated = false;

			uint totalAddedFps = 0;

			ushort samplesToIterateLow1 = _fpsSamplesCount < _low1Samples
				? _fpsSamplesCount : _low1Samples;

			ushort samplesToIterateLow01 = _fpsSamplesCount < _low01Samples
				? _fpsSamplesCount : _low01Samples;

			ushort sampleToStartIn = (ushort)(CAPACITY - _fpsSamplesCount);

			for (ushort i = sampleToStartIn; i < sampleToStartIn + samplesToIterateLow1; i++)
			{
				totalAddedFps += _fpsSamplesSorted[i];

				if (!low01Calculated && i >= samplesToIterateLow01 - 1)
				{
					low01Calculated = true;

					Low01 = (ushort)((float)totalAddedFps / _low01Samples);
				}
			}

			Low1 = (ushort)((float)totalAddedFps / _low1Samples);
		}

		private void StartUpdateMinMax() => _canUpdateMinMax = true;
		#endregion Methods
	}
}