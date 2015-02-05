using UnityEngine;
using System.Collections;

public class ExplosionAnim : MonoBehaviour {

	public float loopduration = 1f;
	public Material ExplosionMat;
	public float size = 0.1f;

	// Use this for initialization
	void Awake () {
		Destroy (gameObject, 4);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float r = Mathf.Sin((Time.time / loopduration) * (2 * Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin((Time.time / loopduration + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin((Time.time / loopduration + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;
		renderer.material.SetVector("_ChannelFactor", new Vector4(r,g,b,0));

		size += 1.2f;
		gameObject.transform.localScale = new Vector3 (size, size, size);
	//	gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, size/8, gameObject.transform.localPosition.z);
		renderer.material.SetVector ("_Range", new Vector3 (size / 300, size / 100, 0) );
		renderer.material.SetFloat ("_ClipRange", (1 - size/400) );
			}
}
