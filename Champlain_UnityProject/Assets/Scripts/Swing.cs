using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {

	public Windspeed wind;
	public GameObject swingRopePrefab;
	public GameObject swingSeatPrefab;

	[SerializeField]
	GameObject[] anchors;
	GameObject[] seatAnchors;
	GameObject seat;

	[Space(20)]
	[SerializeField]
	float ropeLength;

	[SerializeField]
	Vector3 position;

	[SerializeField]
	Vector3 seatSize;





	public void Create(){
		MakeSeat();
		MakeRopes();
	}

	// Use this for initialization
	void Start () {
		Create();		
	}


	public void Destroy(){
		DestroyImmediate(seat);
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

		GameObject newRopeContainer = new GameObject("Swing_Ropes");
		newRopeContainer.transform.parent = gameObject.transform;
	}
}
