using UnityEngine;

namespace Antipixel.DebugSystem
{
	[System.Serializable]
	internal class LogTagData
	{
		#region Fields
		[SerializeField] private string _name;
		[SerializeField] private Color _color = Color.white;
		[SerializeField] private LogTagType _type;
		#endregion Fields


		#region Properties
		public string Name => _name;
		public Color Color => _color;
		public LogTagType Type => _type;
		#endregion Properties
	}
}