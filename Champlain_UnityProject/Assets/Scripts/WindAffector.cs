using UnityEngine;
using System.Collections;

public class WindAffector : MonoBehaviour {

	[SerializeField]
	Rigidbody rigidbody;

	public Windspeed wind;


	void FixedUpdate(){
		UpdateWindforce();
	}

	void UpdateWindforce(){
		Vector3 force = new Vector3(wind.Output*wind.Direction.x, wind.Output*wind.Direction.y, wind.Output*wind.Direction.z);
		rigidbody.AddForce(force);
	}
}
