using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EndWaveScript : MonoBehaviour {

	public float timeToWait;
	public Text textTime;
	public LevelManager manager;

	private float time;
	private float timeEnd;
	private bool isDisplayed = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isDisplayed) {
			time = Time.time;
			int timeRemain = (int)Math.Round(timeEnd - time);
			textTime.text = "Next wave in " + timeRemain + "s";
			if (timeRemain == 0) {
				stopScreen ();
			}
		}
	}

	public void AffScreen() {
		if (isDisplayed == false) {
			gameObject.SetActive (true);
			//manager.EnabledPause ();
			time = Time.time;
			timeEnd = time + timeToWait;
			isDisplayed = true;
		}
	}

	public void stopScreen() {
		if (isDisplayed == true) {
			//manager.DisabledPause ();
			isDisplayed = false;
			gameObject.SetActive (false);
			manager.towerPlacementState ();
		}
	}

}
