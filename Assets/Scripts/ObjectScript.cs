using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectScript : MonoBehaviour {

	public float KillRad = 30f;
	private Vector3 Direction;
	private float Distance;
	private GameObject Explosion;
	private GameObject origin;

	public string DistString;

	private bool doOnce;

	private Interact InteractScript;

	void Awake () {
		origin = GameObject.Find ("origin");
		DistString = (Mathf.Round ((Vector3.Distance (transform.position, origin.transform.position)))).ToString ();

		GameObject InteractHandlerObj = GameObject.Find ("Interact Control");
		InteractScript = InteractHandlerObj.gameObject.GetComponent<Interact> ();
		doOnce = true;
		}

	// Update is called once per frame
	void Update () {

	}

	// Use this for initialization
	public void ExplosionHandler () {
		Explosion = GameObject.FindGameObjectWithTag ("Explosion") ;
		Direction = Explosion.transform.position - transform.position;
		Distance = Direction.magnitude;
		Distance = Direction.magnitude;
		
		if (Distance < KillRad && doOnce == true) {
			Destroy (gameObject, Distance / 2);
			renderer.material.color = Color.black;
			InteractScript.AddScore ();
			doOnce = false;
		}
	}
}