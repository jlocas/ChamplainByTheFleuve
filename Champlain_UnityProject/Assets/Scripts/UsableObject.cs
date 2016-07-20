using UnityEngine;
using System.Collections;


public class UsableObject : MonoBehaviour {

	[SerializeField]
	protected bool isActive = false;

	public void Trigger(){
		if(isActive){
			Deactivate();
		} else {
			Activate();
		}
	}

	protected void Activate(){
		Debug.Log("L'OBJET " + gameObject.name + " est activé!");
		isActive = true;
	}

	protected void Deactivate(){
		Debug.Log("L'OBJET " + gameObject.name + " est désactivé!");
		isActive = false;
	}
}
