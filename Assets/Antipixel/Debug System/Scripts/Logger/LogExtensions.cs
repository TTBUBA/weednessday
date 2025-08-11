using System;
using UnityEngine;
using Antipixel.DebugSystem;
using Object = UnityEngine.Object;

public static class LogExtensions
{
	public static void Log(this Object obj, params object[] message) => Log(obj, Tag.None, message);
	public static void Log(this Object obj, Tag logTag, params object[] message)
	{
		LogTagData tagData = Array.Find(DebugSystemSettings.Instance.tags, tag => tag.Name == logTag.ToString());

		Action<string, Object> function = function = tagData?.Type switch
		{
			LogTagType.Warning => Debug.LogWarning,
			LogTagType.Error => Debug.LogError,
			_ => Debug.Log,
		};

		string prefix = string.Empty;
		if (tagData != null)
			prefix = Tint(tagData.Name, tagData.Color);

		string prefixFormat = prefix == string.Empty ? prefix : $"[{prefix}] ";
		string contextObjectFormat = $"[{Tint(obj.name, DebugSystemSettings.Instance.defaultColor)}]";
		string messagesFormat = $"{string.Join(" | ", message)}";
		function($"{prefixFormat}{contextObjectFormat} {messagesFormat}", obj);

		static string Tint(string msg, Color col) => $"<color=#{ColorUtility.ToHtmlStringRGB(col)}>{msg}</color>";
	}
}