using UnityEngine;
using System.Collections;

public class KeyDuringGames : MonoBehaviour {

    

    private Canvas pauseCanvas;
    private LevelManager levelManager;


    //bool inGame = false;
    
	// Use this for initialization
	void Start () {
        pauseCanvas = GameObject.Find("PauseMenuCanvas").GetComponent<Canvas>();
        levelManager = GameObject.Find("_SCRIPTS_").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
/*
        if (Input.GetKeyUp(KeyCode.P)) {
             PauseCanvas.enabled = false;
        }*/
        if (Input.GetKeyUp(KeyCode.Escape)) {
            levelManager.EnabledPause();
            pauseCanvas.enabled = true;
        }
	}
    
    public void SetGameStatus(bool isInGame) {
        //inGame = isInGame;
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
