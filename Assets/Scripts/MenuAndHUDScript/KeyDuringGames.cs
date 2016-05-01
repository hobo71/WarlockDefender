using UnityEngine;
using System.Collections;

public class KeyDuringGames : MonoBehaviour {

    private Canvas PauseCanvas;
    
    bool inGame = false;
    
	// Use this for initialization
	void Start () {
        PauseCanvas = GameObject.Find("PauseMenuCanvas").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
/*
        if (Input.GetKeyUp(KeyCode.P)) {
             PauseCanvas.enabled = false;
        }*/
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Cursor.visible = true;
             PauseCanvas.enabled = true;
        }
	}
    
    public void SetGameStatus(bool isInGame) {
        inGame = isInGame;
    }
    
    public void QuitGame() {
        Application.Quit();
    }
}
