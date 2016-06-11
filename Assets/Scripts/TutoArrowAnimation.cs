using UnityEngine;
using System.Collections;

public class TutoArrowAnimation : MonoBehaviour {

	private bool right = true;
	[SerializeField] float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = gameObject.GetComponent<RectTransform> ().localPosition;
		if (pos.x > 25) {
			right = false;
		}
		if (pos.x < -25) {
			right = true;
		}
		if (right == true) {
			gameObject.GetComponent<RectTransform> ().localPosition += new Vector3 (speed, 0.0f, 0.0f);
		} else {
			gameObject.GetComponent<RectTransform> ().localPosition += new Vector3 (-speed, 0.0f, 0.0f);		
		}
	}
}
