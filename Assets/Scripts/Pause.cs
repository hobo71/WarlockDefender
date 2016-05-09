using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public Canvas canvas;
	private bool isPaused = false;
	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape))
		{
			MenuDisplay();
			if (isPaused) {
				levelManager.EnabledPause ();
			} else {
				levelManager.DisabledPause ();
			}
		}
	}

	public void MenuDisplay()
	{
		isPaused = !isPaused;
		canvas.enabled = isPaused;
	}
}
