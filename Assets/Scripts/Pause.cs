using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

	public Canvas canvas;
	private bool isPaused = false;
	public LevelManager levelManager;
	public bool canBeActivated = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (canBeActivated == true && Input.GetKeyUp (KeyCode.Escape))
		{
			if (isPaused == false) {
				isPaused = true;
				canvas.enabled = true;
				levelManager.EnabledPause ();	
			}
		}
	}
	
	public void DisablePause()
	{
		if (isPaused == true) {
			isPaused = false;
			canvas.enabled = false;
			levelManager.DisabledPause ();	
		}
	}
	
	public void GoToMenu()
	{
		levelManager.DisabledPause ();
		levelManager.CursorVisible ();
		levelManager.LoadGameScene ("Menu");
	}
}
