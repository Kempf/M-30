using UnityEngine;
using System.Collections;

public class ElevationScript : MonoBehaviour {

	private float maxElevation = 63.5f;
	private float minElevation = -3f;

	public GameObject Pivot;

	public float Elevation;

	private float ElevationMod;

	public float speed;

	// Use this for initialization
	void Start () {
	
		Elevation = 0f;

	}
	
	// Update is called once per frame
	public void Interact1 () {

		if (Input.GetButton ("Focus"))
						speed = 100;
				else
						speed = 10;

		//elev value
		ElevationMod = (Input.GetAxis ("UpDown"))/speed;
		//limits
		if (Elevation <= maxElevation && Elevation >= minElevation)
						Elevation += ElevationMod;
		//limits
		if (Elevation > maxElevation)
						Elevation = maxElevation;
		//more limits
		if (Elevation < minElevation)
			Elevation = minElevation;
		//applying rotation
		Pivot.transform.localEulerAngles = new Vector3 (Elevation, 0f, 0f);
		transform.localEulerAngles = new Vector3 (Elevation * 20f, 0f, 0f);
	}
}
