using UnityEngine;
using System.Collections;

public enum RTPCList{
	Windspeed,
	WindspeedFiltered,
	OutdoorIndoor
}

public class WwiseRTPCController : MonoBehaviour {

	[SerializeField]
	private RTPC windspeed;
	public RTPC Windspeed{
		get{
			return windspeed;
		}
		set{
			windspeed = value;
		}
	}

	[SerializeField]
	private RTPC windspeedFiltered;
	public RTPC WindspeedFiltered{
		get{
			return windspeedFiltered;
		}
		set{
			windspeedFiltered = value;
		}
	}

	[SerializeField]
	public RTPC outdoorIndoor;
	public RTPC OutdoorIndoor{
		get{
			return outdoorIndoor;
		}
		set{
			outdoorIndoor = value;
		}
	}

	public RTPC GetRTPC(RTPCList choice){
		switch(choice){
		case RTPCList.Windspeed:
			return windspeed;
			break;
		case RTPCList.WindspeedFiltered:
			return windspeedFiltered;
			break;
		case RTPCList.OutdoorIndoor:
			return outdoorIndoor;
			break;
		}
		return null;
	}
}
