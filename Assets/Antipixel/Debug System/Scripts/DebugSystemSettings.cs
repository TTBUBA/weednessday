using UnityEngine;
using UnityEditor;

namespace Antipixel.DebugSystem
{
	internal class DebugSystemSettings : ScriptableObject
	{
		#region Constants
		internal const string NAME = "Debug System";
		internal const string SETTINGS = "DebugSystemSettings";
		internal static readonly string PATH = $"Assets/Antipixel/{NAME}/Resources/{SETTINGS}.asset";
		#endregion Constants


		#region Fields
		[SerializeField] internal Color defaultColor = Color.white;
		[SerializeField] internal LogTagData[] tags;
		#endregion Fields


		#region Properties
		internal static DebugSystemSettings Instance
		{
			get
			{
				DebugSystemSettings settings;

#if UNITY_EDITOR
				settings = AssetDatabase.LoadAssetAtPath<DebugSystemSettings>(PATH);

				if (settings == null)
				{
					settings = CreateInstance<DebugSystemSettings>();
					AssetDatabase.CreateAsset(settings, PATH);
					AssetDatabase.SaveAssets();
				}
#else
				settings = Resources.Load<DebugSystemSettings>(SETTINGS);
#endif

				return settings;
			}
		}
		#endregion Properties
	}
}