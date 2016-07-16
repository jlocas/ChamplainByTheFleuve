using UnityEngine;
using System.Collections;

public class PeopleRows : MonoBehaviour {

	public GameObject capsule;
	public float separation = 1;
	public int rows;
	public int columns;

	// Use this for initialization
	public void Build () {
		for(int i=0; i<rows; i++){
			for(int j=0; j<columns; j++){
				Vector3 pos = gameObject.transform.position;
				Vector3 size = new Vector3();

				pos.x += i * separation;
				pos.z += j * separation;

				GameObject newGuyORGirl = Instantiate(capsule, pos, Quaternion.identity) as GameObject;
				newGuyORGirl.name = string.Format("Person {0}", i+j+i*j);
				newGuyORGirl.transform.parent = gameObject.transform;
			}
		}
	
	}
	
	// Update is called once per frame
	public void Destroy () {
		int count = gameObject.transform.childCount;
		for(int i=0; i<count; i++){
			DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
		}
	}
}
