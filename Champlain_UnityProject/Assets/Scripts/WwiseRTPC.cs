using UnityEngine;
using System.Collections;

[System.Serializable]
public class RTPC {
	
	public string name;
	[SerializeField]
	private float output;
	public float Output{
		get{
			return output;
		}
		set{
			output = value;
			SendValue();
		}
	}

	public void SendValue(){
		AkSoundEngine.SetRTPCValue(name, output);
	}
}