using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour {

	public Text BearingDiv;
	public Text BearingDeg;
	private float BearDiv;
	private float BearDeg;

	// Update is called once per frame
	void Update () {
	
		BearDeg = transform.rotation.eulerAngles.y;
		BearDiv = Mathf.Round (BearDeg * 16.666f);
        if (BearDiv > 3000) {
            BearDiv -= 6000;
        }
		BearingDeg.text = Mathf.Round (BearDeg).ToString();
		BearingDiv.text = BearDiv.ToString();

	}
}
