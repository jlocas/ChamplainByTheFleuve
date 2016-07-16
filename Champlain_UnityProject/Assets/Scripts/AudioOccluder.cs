using UnityEngine;
using System.Collections;

public class AudioOccluder : MonoBehaviour {

	public float maxDistance;
	public GameObject player;
	public AkGameObj akObj;
	bool isOccluded;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CheckOcclusion(){
		if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= maxDistance){
			RaycastHit rc;
			Physics.Raycast(gameObject.transform.position, player.transform.position, out rc);

			if(rc.collider.tag == "Player" && isOccluded){
				isOccluded = false;
			} else if(!isOccluded) {
				isOccluded = true;
			}
		}

	}

	void SetOcclusion(bool occ){

	}

}
