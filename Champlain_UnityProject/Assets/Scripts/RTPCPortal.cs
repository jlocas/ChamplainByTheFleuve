//////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2014 Audiokinetic Inc. / All Rights Reserved
//
//////////////////////////////////////////////////////////////////////

using UnityEngine;
using System;
using System.Collections.Generic;

public enum Axis{
x,
y,
z
}

[RequireComponent (typeof(BoxCollider))]
//[RequireComponent (typeof(Rigidbody))]
public class RTPCPortal : MonoBehaviour
{
	public GameObject player;
	public AkAudioListener listener;
	Collider listenerCollider;
	public WwiseRTPCController rtpcController;
	BoxCollider collider;

	[Space(10)]
	public RTPCList linkTo;

	[SerializeField]
	float output;
	float Output{
		get{
			return output;
		}
		set {
			rtpc.Output = value;
			output = value;
		}
	}

	RTPC rtpc;
	public Axis axis = Axis.x;
	Vector3 _axis;


	public void Start(){
		collider = GetComponent<BoxCollider>();
		listenerCollider = player.GetComponent<Collider>();
		SetAxis();

		rtpc = rtpcController.GetRTPC(linkTo);
		enabled = false;
	}


	void Update(){
		Output = GetRTPCValue(listener.transform.position);
	}

	void OnTriggerEnter(Collider other){
		if(other == listenerCollider){
			this.enabled = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other == listenerCollider){
			this.enabled = false;
			Output = Mathf.Round( GetRTPCValue(listener.transform.position) );
		}
	}

	void SetAxis(){
		switch(axis){
		case Axis.x:
			_axis = new Vector3(1,0,0);
			break;
		case Axis.y:
			_axis = new Vector3(0,1,0);
			break;
		case Axis.z:
			_axis = new Vector3(0,0,1);
			break;
		}
	}

	public float GetRTPCValue(Vector3 in_position){
		//total lenght of the portal in the direction of axis
		float portalLength = Vector3.Dot( Vector3.Scale(collider.size, transform.lossyScale), _axis);

		//transform axis to world coordinates 
		Vector3 axisWorld = Vector3.Normalize(transform.rotation * _axis);

		//Get distance form left side of the portal(opposite to the direction of axis) to the game object in the direction of axisWorld
		float portalWidth = Vector3.Dot (in_position - (transform.position - (portalLength * 0.5f * axisWorld)), axisWorld);

		return Mathf.Clamp01( ((portalLength - portalWidth) * (portalLength - portalWidth)) / (portalLength*portalLength) );
	}
}