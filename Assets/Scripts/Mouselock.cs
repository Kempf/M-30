using UnityEngine;
using System.Collections;

public class Mouselock : MonoBehaviour {

	public GameObject Image;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Screen.lockCursor = true;
		Screen.showCursor = false;

		if (Input.GetButtonDown ("MapBag")) {
			Screen.lockCursor = !Screen.lockCursor;
			Screen.showCursor = !Screen.showCursor;
		}

		Image.transform.position = Vector3.Lerp (Image.transform.position, Input.mousePosition, .1f);

	}
}
