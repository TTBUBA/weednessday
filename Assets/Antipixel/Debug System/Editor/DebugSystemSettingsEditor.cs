#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Antipixel.DebugSystem
{
	internal class DebugSystemSettingsEditor : SettingsProvider
	{
		#region Fields
		private static SerializedObject _serializedObject;
		#endregion Fields


		#region Constructors
		private DebugSystemSettingsEditor(string path, SettingsScope scope = SettingsScope.Project)
			: base(path, scope) { }
		#endregion Constructors


		#region Properties
		private static DebugSystemSettings Settings => DebugSystemSettings.Instance;
		#endregion Properties


		#region Methods
		public override void OnActivate(string searchContext, VisualElement rootElement)
			=> _serializedObject = new SerializedObject(Settings);

		public override void OnGUI(string searchContext)
		{
			EditorGUILayout.PropertyField(_serializedObject.FindProperty("defaultColor"), Styles.objectColor);
			EditorGUILayout.PropertyField(_serializedObject.FindProperty("tags"), Styles.tags);

			if (GUILayout.Button("Apply")) Apply();

			_serializedObject.ApplyModifiedPropertiesWithoutUndo();
		}

		[SettingsProvider]
		private static SettingsProvider CreateSettingsProvider()
		{
			bool isSettingsAvaiable = File.Exists(DebugSystemSettings.PATH);

			if (!isSettingsAvaiable)
				_serializedObject = new SerializedObject(Settings);

			if (isSettingsAvaiable)
			{
				var provider = new DebugSystemSettingsEditor($"Project/Antipixel/{DebugSystemSettings.NAME}", SettingsScope.Project);

				provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
				return provider;
			}

			return null;
		}

		private void Apply()
		{
			string enumName = "Tag";
			string path = $"Assets/Antipixel/Systems/{DebugSystemSettings.NAME}/Resources/{enumName}.cs";
			TextAsset textAsset = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;

			Regex regex = new Regex("[^a-zA-Z0-9_]");
			IEnumerable<string> values = Settings.tags
				.Select(i => regex.Replace(
					RemoveFirstDigits(i.Name),
					string.Empty))
				.Distinct()
				.OrderBy(i => i);

			string content = $"public enum {enumName} {{ None, VALUES }}"
				.Replace("VALUES", string.Join(", ", values));
			File.WriteAllText(path, content);

			AssetDatabase.Refresh();
		}

		private string RemoveFirstDigits(string str)
		{
			string result = "";
			bool letters = false;
			foreach (char c in str)
			{
				if (char.IsDigit(c) && !letters) continue;
				else letters = true;
				result += c;
			}
			return result;
		}
		#endregion Methods


		#region Classes
		private class Styles
		{
			internal static GUIContent objectColor = new GUIContent("Default Color");
			internal static GUIContent tags = new GUIContent("Tags");
		}
		#endregion Classes
	}
}
#endif