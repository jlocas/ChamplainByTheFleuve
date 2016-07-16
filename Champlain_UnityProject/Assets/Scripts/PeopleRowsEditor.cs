using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PeopleRows))]
public class PeopleRowsEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PeopleRows myScript = (PeopleRows)target;
		if(GUILayout.Button("Build People"))
		{
			myScript.Build();
		}
		if(GUILayout.Button("Destroy People"))
		{
			myScript.Destroy();
		}
	}
}
