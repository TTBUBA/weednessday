using UnityEngine;

namespace Example.AntipixelDebugSystem
{
	public class DebugSystemExample : MonoBehaviour
	{
		private void Start()
		{
			this.Log("Hello World!", "A", "B", "C");
			this.Log(Tag.Notification, "Hello World!");
			this.Log(Tag.Success, "Hello World!");
			this.Log(Tag.Warning, "Hello World!");
			this.Log(Tag.Error, "Hello World!");
		}
	}
}