using UnityEngine;
using System.Collections;

public class ElevationScript : MonoBehaviour {

	private float maxElevation = 63.5f;
	private float minElevation = -3f;
	public GameObject Pivot;
	public float ElevationAngle;
	private float ElevationMod;
	public float speed;

	void Update () {
		Interact.ElevDiv = Mathf.Round (ElevationAngle * 16.666f);
	}

	[RPC]
	public void Elevation(float ElevationMod){
		
		//limits
		if (ElevationAngle <= maxElevation && ElevationAngle >= -maxElevation)
			ElevationAngle += ElevationMod;
		//limits
		if (ElevationAngle > maxElevation)
			ElevationAngle = maxElevation;
		//more limits
		if (ElevationAngle < minElevation)
			ElevationAngle = minElevation;
		//applying rotation
		Pivot.transform.localEulerAngles = new Vector3 (ElevationAngle, 0f, 0f);
		transform.localEulerAngles = new Vector3 (ElevationAngle * 20f, 0f, 0f);
	}
}
