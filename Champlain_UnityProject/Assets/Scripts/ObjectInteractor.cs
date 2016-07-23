using UnityEngine;
using System.Collections;

public class ObjectInteractor : MonoBehaviour {

	public Camera camera;
	public float maxDistance;

	
	// Update is called once per frame
	void Update () {
		KeyCheck();
	}

	void KeyCheck(){
		if(Input.GetKeyDown(KeyCode.E)){
			CastRay();
		}
	}

	void CastRay(){
		Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width*0.5f, Screen.height*0.5f, 0f));		
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, maxDistance)){
			
			Collider col = hit.collider;

			if( col.GetComponent<UsableObject>() ){
				
				UsableObject uo = col.GetComponent<UsableObject>();
				uo.Trigger();
			}
		}
	}
}
