using UnityEngine;
using System.Collections;

public class Windspeed : MonoBehaviour {
	public WwiseRTPCController rtpc;
	public WindZone windzone;

	[Space(10)]
	[SerializeField]
	private float output;
	public float Output{
		get{
			return output;
		}
	}

	[Space(10)]
	[MinMaxRangeAttribute(0f, 100f)]
	public MinMaxRange range;
	[SerializeField]
	private float baseFreq;
	[SerializeField]
	private float octaveRatio;
	[SerializeField]
	private float octaveDamp;
	[SerializeField]
	private float mul;

	[SerializeField]
	private LerpNoise[] lerpers = new LerpNoise[4];

	[SerializeField]
	Vector3 direction;

	public Vector3 Direction{
		get{
			return direction;
		}
	}

	float maxPossible = 0f;
	float maxDiv;

	//windzone lerp
	float wzlStart = 0f;
	float wzlTarget = 0f;
	float wzlStartTime = 0f;
	float wzlDistance = 0f;


	// Use this for initialization
	void Start () {
		for(int i=0; i<lerpers.Length; i++){
			lerpers[i] = new LerpNoise(baseFreq * Mathf.Pow(2, i), Mathf.Pow(0.5f, i));
			maxPossible += lerpers[i].Mul;

		}
		maxDiv = 1f / maxPossible;

		//InvokeRepeating("UpdateWindzone", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		float sum = 0f;
		foreach(LerpNoise ler in lerpers){
			ler.Lerp();
			sum += ler.Output;
		}

		output = sum * maxDiv * (range.max - range.min) + range.min;
		rtpc.Windspeed.Output = output;
		rtpc.WindspeedFiltered.Output = output;

		UpdateWindzone();

	}

	void UpdateWindzone(){
		windzone.windMain = output * 0.03f;
		windzone.windTurbulence = output * 0.01f;
	}

	/*
	void UpdateWindzone(){

		wzlStart = wzlTarget;
		wzlTarget = output;

		wzlStartTime = Time.time;
		wzlDistance = Mathf.Abs(wzlTarget - wzlStart);
	}

	void LerpWindzone(){
		float distCovered = (Time.time - wzlStartTime);
		float perc = distCovered / wzlDistance;

		float result = Mathf.Lerp(wzlStart, wzlTarget, perc);

		windzone.windMain = result * 0.01f;
		windzone.windTurbulence = result * 0.001f;
	}
*/
}
