using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Swing))]
public class SwingEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Swing myScript = (Swing)target;
		if(GUILayout.Button("Create Swing"))
		{
			myScript.Create();
		}
		if(GUILayout.Button("Destroy Swing"))
		{
			myScript.Destroy();
		}
	}
}
