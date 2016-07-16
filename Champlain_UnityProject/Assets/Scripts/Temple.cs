using UnityEngine;
using System.Collections;

public class Temple : MonoBehaviour {

	public GameObject cube;
	public GameObject cylinder;
	public Material marbleMaterial;
	[Space(10)]
	public Vector3 position;
	public Vector3 innerSize; // size of the walls
	public Vector3 lowerSize; // size at the bottom of the stairs
	public Vector3 upperSize; // size at the top of the stairs

	[Space(10)]
	public float wallThickness = 1f;
	[Space(10)]
	public float stairsHeight;
	public float stairsAmt;
	[Space(10)]
	public float doorHeight;
	public float doorWidth;
	[Space(10)]
	public int roofSteps;
	public float roofHeight;
	[Space(10)]
	public int columnsPerSide;
	public float columnHeight;
	public float columnRadius;

	// Use this for initialization
	public void Build () {
		CreateFloor();
		CreateWalls();
		CreateColumns();
		CreateRoof();
	}

	public void Destroy(){
		int count = gameObject.transform.childCount;
		for(int i=0; i<count; i++){
			DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
		}
	}

	void CreateFloor(){
		GameObject stairsContainer = new GameObject();
		stairsContainer.name = "Stairs";
		stairsContainer.transform.parent = gameObject.transform;

		for(int i=0; i<stairsAmt; i++){
			Vector3 pos = position;
			pos.y = i * stairsHeight;

			Vector3 size = new Vector3(0, stairsHeight, 0);
			size.x = Mathf.Lerp(lowerSize.x, upperSize.x, i/stairsAmt);
			size.z = Mathf.Lerp(lowerSize.z, upperSize.z, i/stairsAmt);

			GameObject newStep = Instantiate(cube, pos, Quaternion.identity) as GameObject;
			newStep.name = string.Format("Step {0}", i+1);
			newStep.transform.parent = stairsContainer.transform;

			newStep.transform.localScale = size;
		}
	}

	void CreateWalls(){
		GameObject wallContainer = new GameObject();
		wallContainer.name = "Walls";
		wallContainer.transform.parent = gameObject.transform;

		//the 3 walls that dont have doors
		for(int i=0; i<3; i++){
			Vector3 pos = position;
			pos.y = stairsHeight * stairsAmt + innerSize.y*0.5f - 0.5f;
			Vector3 size = new Vector3();
			size.y = innerSize.y;

			switch(i)
			{
			case 0:
				pos.z += 0.5f*innerSize.z;

				size.x = innerSize.x + wallThickness;
				size.z = wallThickness;

				break;
			case 1:
				pos.z -= 0.5f*innerSize.z;

				size.x = innerSize.x + wallThickness;
				size.z = wallThickness;
				break;
			case 2:
				pos.x += 0.5f*innerSize.x;

				size.x = wallThickness;
				size.z = innerSize.z + wallThickness;
				break;
			}

			GameObject newWall = Instantiate(cube, pos, Quaternion.identity) as GameObject;
			newWall.name = string.Format("Wall {0}", i+1);
			newWall.transform.parent = wallContainer.transform;

			newWall.transform.localScale = size;
		}

		//the final wall split in 2 part for the doorway
		for(int i=0; i<2; i++){
			Vector3 pos = position;
			pos.y = stairsHeight * stairsAmt + innerSize.y*0.5f - 0.5f;
			pos.x -= 0.5f*innerSize.x;
			pos.z -= innerSize.z*0.5f*i - innerSize.z*0.25f - doorWidth*0.25f + doorWidth*0.5f*i;

			Vector3 size = new Vector3();
			size.y = innerSize.y;

			size.x = wallThickness;
			size.z = (innerSize.z + wallThickness) * 0.5f - doorWidth*0.5f;

			GameObject newWall = Instantiate(cube, pos, Quaternion.identity) as GameObject;
			newWall.name = string.Format("Wall 3_{0}", i+1);
			newWall.transform.parent = wallContainer.transform;

			newWall.transform.localScale = size;
		}

		//the final block on top of the doorway
		for(int i=0; i<1; i++){
			Vector3 pos = position;
			pos.y = stairsHeight * stairsAmt + innerSize.y*0.5f - 0.5f + doorHeight*0.5f;
			pos.x -= 0.5f*innerSize.x;

			Vector3 size = new Vector3();
			size.y = innerSize.y - doorHeight;
			size.x = wallThickness;
			size.z = doorWidth;

			GameObject newWall = Instantiate(cube, pos, Quaternion.identity) as GameObject;
			newWall.name = "Wall 4";
			newWall.transform.parent = wallContainer.transform;

			newWall.transform.localScale = size;
		}
	}

	void CreateRoof(){
		GameObject roof = Instantiate(cube) as GameObject;
		roof.name = "Roof";
		roof.transform.parent = gameObject.transform;

		Vector3 pos = position;
		pos.y = stairsHeight * stairsAmt + innerSize.y;

		Vector3 size = new Vector3();
		size.y = roofHeight;
		size.x = upperSize.x;
		size.z = upperSize.z;

		roof.transform.localScale = size;
		roof.transform.position = pos;
		
	}

	void CreateColumns(){
		GameObject columnContainer = new GameObject();
		columnContainer.name = "Columns";
		columnContainer.transform.parent = gameObject.transform;

		//4 corners
		for(int i=0; i<4; i++){
			Vector3 pos = position;
			pos.y = stairsHeight * stairsAmt + columnHeight*0.5f - 0.5f;

			switch(i){
			case 0:
				pos.x += upperSize.x*0.5f - columnRadius*0.5f;
				pos.z += upperSize.z*0.5f - columnRadius*0.5f;
				break;
			case 1:
				pos.x += upperSize.x*0.5f - columnRadius*0.5f;
				pos.z -= upperSize.z*0.5f - columnRadius*0.5f;
				break;
			case 2:
				pos.x -= upperSize.x*0.5f - columnRadius*0.5f;
				pos.z += upperSize.z*0.5f - columnRadius*0.5f;
				break;
			case 3:
				pos.x -= upperSize.x*0.5f - columnRadius*0.5f;
				pos.z -= upperSize.z*0.5f - columnRadius*0.5f;
				break;
			}

			Vector3 size = new Vector3(columnRadius, columnHeight*0.5f, columnRadius);

			GameObject newCol = Instantiate(cylinder, pos, Quaternion.identity) as GameObject;
			newCol.name = string.Format("Column {0}", i+1);
			newCol.transform.parent = gameObject.transform;

			newCol.transform.localScale = size;
		}

	}
}
