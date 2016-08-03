using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour{

	[SerializeField]
	Material material; //rope material

	[Space(20)]
	[SerializeField]
	Rigidbody startBody; //which body to anchor to
	[SerializeField]
	GameObject startAnchor; //where the anchor is on the rigidbody;

	[Space(20)]
	[SerializeField]
	Rigidbody endBody; //which body to anchor to
	[SerializeField]
	GameObject endAnchor; //where the anchor is on the rigidbody;

	[Space(20)]
	[SerializeField]
	int pointCount; //how many points make the rope
	[SerializeField]
	float overlap;
	[SerializeField]
	float mass; //how much weigth is at the end of the rope

	[SerializeField]
	float ropeWidth; //the apparent width of the rope

	[SerializeField]
	GameObject[] points;
	LineRenderer[] lines;
	Rigidbody[] bodies;
	float segmentLength;

	void MakePoints(){ //Make gameobjects which we will use to get the positions of the points in space
		points = new GameObject[pointCount];

		//Assign the anchors to the first and last point of the array
		points[0] = startAnchor;
		points[pointCount-1] = endAnchor;


		for (int i = 1; i < pointCount-1; i++) { //for every point except the first and last (the anchors)
			GameObject newPoint = new GameObject();
			newPoint.transform.parent = gameObject.transform;
			newPoint.name = string.Format("RopePoint_{0}", i);

			//add 1 to have points only between the anchors
			newPoint.transform.position = Vector3.Lerp( startAnchor.transform.position, endAnchor.transform.position, (float)(i+1) / (float)(pointCount+1) );

			points[i] = newPoint;
		}
	}

	void MakeJoints(){
		for (int i = 1; i < pointCount-2; i++) {
			AddJoint(points[i], points[i+1]);
		}


		AddJoint( 
			startAnchor,
			startBody,
			points[1],
			points[1].GetComponent<Rigidbody>() 
		);

		AddJoint( 
			points[pointCount-2],
			points[pointCount-2].GetComponent<Rigidbody>(),
			endAnchor,
			endBody
		);

	}

	void AddJoint(GameObject go, GameObject target){
		HingeJoint joint = go.AddComponent<HingeJoint>();
		joint.autoConfigureConnectedAnchor = false;

		joint.connectedBody = target.GetComponent<Rigidbody>();
		joint.connectedAnchor = new Vector3(0f, -0.5f * target.transform.localPosition.y, 0f);

		joint.anchor = new Vector3(0, 0.5f * target.transform.localPosition.y, 0f);

		joint.axis = Vector3.one;

		joint.useLimits = false;
		joint.enableCollision = false;
	}

	void AddJoint(GameObject startPoint, Rigidbody startBody, GameObject targetPoint, Rigidbody targetBody){
		HingeJoint joint = startBody.gameObject.AddComponent<HingeJoint>();
		joint.autoConfigureConnectedAnchor = false;

		joint.connectedBody = targetBody;
		joint.connectedAnchor = targetBody.transform.InverseTransformPoint(targetPoint.transform.position);

		joint.anchor = startBody.transform.InverseTransformPoint(startPoint.transform.position);

		joint.axis = Vector3.one;

		joint.useLimits = false;
		joint.enableCollision = false;
	}

	void MakeRigidbodies(){
		bodies = new Rigidbody[pointCount];

		bodies[0] = startBody;
		bodies[pointCount-1] = endBody;

		for (int i = 1; i < pointCount-1; i++) { //dont add rigidbodies to the anchors since they are attached to different rigidbodies from the anchors
			Rigidbody body = points[i].AddComponent<Rigidbody>();
			body.mass = mass;
		}
	}

	void MakeLineRenderers(){

		lines = new LineRenderer[pointCount];

		for (int i = 0; i < pointCount-1; i++) {
			AddLineRenderer(points[i], points[i+1], i);
		}
	}

	void AddLineRenderer(GameObject go, GameObject target, int i){
		LineRenderer newLine = go.AddComponent<LineRenderer>();
		newLine.material = material;

		newLine.SetPosition(0, new Vector3( 
			go.transform.position.x,
			go.transform.position.y * (-0.5f + overlap ),
			go.transform.position.z
		));

		newLine.SetPosition(1, new Vector3( 
			target.transform.position.x,
			target.transform.position.y * (0.5f - overlap ),
			target.transform.position.z
		));
		newLine.SetWidth(ropeWidth, ropeWidth);

		lines[i] = newLine;
	}

	void UpdateLines(){
		for (int i = 0; i < pointCount-1; i++) {
			lines[i].SetPosition(0, points[i].transform.position);
			lines[i].SetPosition(1, points[i+1].transform.position);
		}
	}

	void SetParentage(){
		for (int i = 1; i < pointCount-1; i++) {
			points[i].transform.parent = points[i-1].transform;
		}
	}

	void Start(){
		pointCount += 2; //Make room for the anchors in the array of points
		segmentLength = Vector3.Distance(startAnchor.transform.position, endAnchor.transform.position) / (float)pointCount;

		MakePoints();
		MakeRigidbodies();
		SetParentage();
		MakeJoints();
		MakeLineRenderers();
	}

	void Update(){
		UpdateLines();
	}


	/*
	public GameObject startAnchor;
	public Rigidbody startBody;
	public GameObject endAnchor;
	public Rigidbody endBody;
	public Material material;

	[SerializeField]
	int pointCount;
	[SerializeField]
	float ropeWidth;
	[SerializeField]
	float ropeMass;
	[SerializeField]
	float overlap; //between 0 and 1
	[SerializeField]
	float ropeLength;
	float segmentLength;

	GameObject[] points;
	LineRenderer[] lines;



	// Use this for initialization
	void Start () {
		segmentLength = ropeLength / pointCount;

		MakePoints();
		MakeJoints();
		MakeLineRenderers();
		SetParentage();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLine();
	}

	void MakePoints(){
		pointCount += 2;
		points = new GameObject[pointCount];

		points[0] = startAnchor;
		points[pointCount-1] = endAnchor;

		for (int i = 1; i < pointCount-1; i++) {
			GameObject newPoint = new GameObject();
			newPoint.transform.parent = gameObject.transform;
			newPoint.name = string.Format("RopePoint_{0}", i);

			//+1 because point 1 is the start anchor, points+2 because we have 2 anchors that count as point, we want x points in between them
			newPoint.transform.position = Vector3.Lerp( startAnchor.transform.position, endAnchor.transform.position, (float)(i+1) / (float)(pointCount+1) ); 
			newPoint.transform.localScale = new Vector3(ropeWidth, segmentLength, ropeWidth);

			Rigidbody body = newPoint.AddComponent<Rigidbody>();
			body.mass = ropeMass;

			points[i] = newPoint;
		}
	}

	void MakeJoints(){
		for (int i = 0; i < pointCount-1; i++) {
			AddJoint(points[i], points[i+1], i);


		}
	}

	void UpdateLine(){
		for (int i = 0; i < pointCount-2; i++) {
			lines[i].SetPosition(0, points[i].transform.position);

			lines[i].SetPosition(1, points[i+1].transform.position);

		}

		int last = pointCount-2;
		Vector3 lastPos = new Vector3(
			points[last].transform.position.x + endAnchorOffset.x,
			points[last].transform.position.y + endAnchorOffset.y,
			points[last].transform.position.z + endAnchorOffset.z
		);

		lines[last].SetPosition(0, points[last].transform.position);
		lines[last].SetPosition(1, lastPos);

	}

	void MakeLineRenderers(){

		lines = new LineRenderer[pointCount];

		for (int i = 0; i < pointCount-1; i++) {
			AddLineRenderer(points[i], points[i+1], i);
		}
	}

	void SetParentage(){
		for (int i = 1; i < pointCount-1; i++) {
			points[i].transform.parent = points[i-1].transform;
		}
	}

	void AddLineRenderer(GameObject go, GameObject target, int i){
		LineRenderer newLine = go.AddComponent<LineRenderer>();
		newLine.material = material;
		newLine.SetPosition(0, go.transform.position);
		newLine.SetPosition(1, target.transform.position);
		newLine.SetWidth(ropeWidth, ropeWidth);

		lines[i] = newLine;
	}

	void AddJoint(GameObject go, GameObject target, int i){
		HingeJoint joint = go.AddComponent<HingeJoint>();
		joint.autoConfigureConnectedAnchor = false;

		if(i < pointCount-2){
			joint.connectedAnchor = new Vector3(0f, -0.5f + overlap, 0f);
		} else {
			joint.connectedAnchor = endAnchorOffset;
		}

		joint.anchor = new Vector3(0, 0.5f - overlap, 0f);
		joint.connectedBody = target.GetComponent<Rigidbody>();
		joint.axis = Vector3.one;

		joint.useLimits = false;
		joint.enableCollision = false;
	}*/

}
