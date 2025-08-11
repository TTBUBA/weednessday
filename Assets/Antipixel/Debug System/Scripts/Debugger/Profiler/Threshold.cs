using System;
using UnityEngine;
using UnityEngine.Events;

namespace Antipixel.DebugSystem
{
	[Serializable]
	public class Threshold
	{
		#region Fields
		[SerializeField] private string _name;
		[SerializeField] private float _value;
		#endregion Fields


		#region Events
		[SerializeField] private UnityEvent Event;
		#endregion Events


		#region Properties
		public string Name => _name;
		public float Value => _value;
		#endregion Properties


		#region Methods
		public void Suscribe(UnityAction action) => Event.AddListener(action);
		public void Unsuscribe(UnityAction action) => Event.RemoveListener(action);
		internal void Invoke() => Event?.Invoke();
		#endregion Methods
	}
}