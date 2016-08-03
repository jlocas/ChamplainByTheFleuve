using UnityEngine;
using System.Collections;

public class Swing1 : MonoBehaviour {

	public Windspeed wind;
	public GameObject swingRopePrefab;
	public GameObject swingSeatPrefab;

	[SerializeField]
	GameObject[] anchors;
	GameObject[] seatAnchors;
	GameObject seat;

	[Space(20)]

	[SerializeField]
	Vector3 position;
	[SerializeField]
	float ropeLength;
	float ropeSpread;
	LineRenderer[] ropes;

	[SerializeField]
	Vector3 seatSize;





	public void Create(){
		MakeSeat();
		MakeRopes();
		MakeJoints();
	}

	// Use this for initialization
	void Start () {
		Create();		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRopes();
	}

	public void Destroy(){
		DestroyImmediate(seat);
		DestroyImmediate(ropes[0].transform.parent.gameObject);
	}

	void MakeSeat(){
		seatAnchors = new GameObject[2];
		//Add a Game Object to build into
		GameObject newSeat = Instantiate(swingSeatPrefab, gameObject.transform.position + position, anchors[0].transform.parent.rotation) as GameObject;
		newSeat.transform.parent = gameObject.transform;
		newSeat.GetComponent<WindAffector>().wind = wind;

		//scale is proportionnal to the x distance between the 2 anchors 
		float xDist = Mathf.Abs( anchors[0].transform.localPosition.x - anchors[1].transform.localPosition.x );

		print(xDist);

		Vector3 scale = new Vector3(xDist * seatSize.x, xDist * seatSize.y, xDist * seatSize.z );
		newSeat.transform.localScale = scale;


		newSeat.transform.localPosition = new Vector3(
			anchors[0].transform.parent.localPosition.x,
			anchors[0].transform.parent.localPosition.y - ropeLength, 
			anchors[0].transform.parent.localPosition.z
		);

		newSeat.transform.localRotation = new Quaternion(newSeat.transform.localRotation.x, newSeat.transform.localRotation.y, 0f, newSeat.transform.localRotation.w);


		for (int i = 0; i < 2; i++) {
			GameObject newAnchor = new GameObject( "Seat_Anchor_" + (i+1) );
			newAnchor.transform.parent = newSeat.transform;
			newAnchor.transform.localPosition = new Vector3( (i - 0.5f) / seatSize.x, -0.4f, 0f);
			seatAnchors[i] = newAnchor;
		}

		seat = newSeat;
	}

	void MakeRopes(){
		ropes = new LineRenderer[2];

		GameObject newRopeContainer = new GameObject("Swing_Ropes");
		newRopeContainer.transform.parent = gameObject.transform;

		for(int i=0; i<2; i++){
			GameObject newRope = Instantiate(swingRopePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			newRope.name = "Swing_Rope_" + (i+1);
			newRope.transform.parent = newRopeContainer.transform;

			ropes[i] = newRope.GetComponent<LineRenderer>();
			ropes[i].SetPosition(0, anchors[i].transform.position);
			ropes[i].SetPosition(1, seatAnchors[i].transform.position);
		}
	}

	void MakeJoints(){
		for (int i = 0; i < 2; i++) {
			/*
			SpringJoint newJoint = seat.AddComponent<SpringJoint>();
			newJoint.connectedBody = anchors[i].GetComponent<Rigidbody>();
			newJoint.anchor = new Vector3( (i - 0.5f) / seatSize.x, 0.5f, 0f);

			newJoint.autoConfigureConnectedAnchor = false;
			newJoint.connectedAnchor = new Vector3( (i - 0.5f) / seatSize.x, 0.5f, 0f);

			newJoint.spring = 10000f;
			newJoint.minDistance = anchors[i].transform.position.y < anchors[ (i+1) % 2 ].transform.position.y ? ropeLength - Mathf.Abs(anchors[0].transform.position.y - anchors[1].transform.position.y) : ropeLength;
			newJoint.maxDistance = anchors[i].transform.position.y < anchors[ (i+1) % 2 ].transform.position.y ? ropeLength - Mathf.Abs(anchors[0].transform.position.y - anchors[1].transform.position.y) : ropeLength;
			newJoint.tolerance = 0f;
*/
			HingeJoint newJoint = seat.AddComponent<HingeJoint>();
			newJoint.connectedBody = anchors[i].GetComponent<Rigidbody>();
			newJoint.anchor = new Vector3( (i - 0.5f) / seatSize.x, 0.5f, 0f);

			//newJoint.autoConfigureConnectedAnchor = false;
			//newJoint.connectedAnchor = new Vector3( (i - 0.5f) / seatSize.x, 0.5f, 0f);

			newJoint.useLimits = true;

		}
	}

	void UpdateRopes(){
		for (int i = 0; i < 2; i++) {
			ropes[i].SetPosition(1, seatAnchors[i].transform.position);
		}
	}

}
