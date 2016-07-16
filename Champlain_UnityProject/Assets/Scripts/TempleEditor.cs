using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Temple))]
public class TempleEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Temple myScript = (Temple)target;
		if(GUILayout.Button("Build Temple"))
		{
			myScript.Build();
		}
		if(GUILayout.Button("Destroy Temple"))
		{
			myScript.Destroy();
		}
	}
}
