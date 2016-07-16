using UnityEngine;
using System.Collections;

[System.Serializable]
public class LerpNoise {

	[SerializeField]
	private float output;
	public float Output{
		get{
			return output;
		}
	}

	[Space(10)]
	[SerializeField]
	private float freq;
	public float Freq{
		get{
			return freq;
		}
		set{
			freq = value;
			Debug.Log("setting freq");
		}
	}

	[SerializeField]
	private float mul;
	public float Mul{
		get{
			return mul;
		}
		set{
			mul = value;
		}
	}

	float startTime;
	float distance;
	float nextVal;
	float oldVal = 0f;

	public LerpNoise(float f, float m){
		freq = f;
		mul = m;
		NewLerp();
	}

	public void NewLerp(){
		oldVal = nextVal;
		nextVal = Random.value;	

		startTime = Time.time;
		distance = Mathf.Abs(nextVal-oldVal);
	}

	public void Lerp(){
		float distCovered = (Time.time - startTime) * freq;
		float perc = distCovered / distance;
		output = Mathf.Lerp(oldVal, nextVal, perc) * mul;

		if(perc >= 1){
			NewLerp();
		}
	}
}
