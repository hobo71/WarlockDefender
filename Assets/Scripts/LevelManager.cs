using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Camera buildingCamera;
    public Camera playerCamera;
    public GameObject player;
    public GameObject map;
    public GameObject fpsPanel;
    public GameObject towerPlacementPanel;
	public GameObject EndPanelWin;
	public GameObject EndPanelLose;
	public EndWaveScript EndPanelWinWaveScript;
	public Pause pauseScript;
	public int money;
	public int waveNb;

    private ObjectPlacement objectPlacement;
    private FirstPersonController firstPersonController;
    private FirstPersonShooting firstPersonShooting;
    private MoveCamera moveCamera;
    private FPSPanelScript fpsPanelScript;
	private bool isInPause = false;


    private string gameState = "building";

    void Start () {
        objectPlacement = map.GetComponent<ObjectPlacement>();
        firstPersonController = player.GetComponent<FirstPersonController>();
        firstPersonShooting = player.GetComponent<FirstPersonShooting>();
        moveCamera = buildingCamera.GetComponent<MoveCamera>();
        fpsPanelScript = fpsPanel.GetComponent<FPSPanelScript>();
		money = 600;
		waveNb = 1;
    }

	public void reInitStats() {
		money = 600;
		waveNb = 1;
		player.GetComponent<PlayerStats>().ResetLife();
		CastleStats.life = 100;
	}

	void Update () {
        if (Input.GetKeyDown("c"))
            SelectCamera();
		if (gameState != "building" && isInPause == false) {
			if (SpawnManager.isEnemiesAlive () == false) {
				if (SpawnManager.isLastWave ()) {
					EndPanelWin.SetActive (true);
					EnabledPause ();
                    Coins.getCoins();
                    DataContainerScript.instance.UnlockAll();
				} else {
                    fpsPanel.GetComponent<FPSPanelScript>().TurnAllSpellsToFalse();
					EndPanelWinWaveScript.AffScreen ();
                    Coins.getCoins();
                }
			} else if (player.GetComponent<PlayerStats>().GetCurrentLife() <= 0 || CastleStats.life <= 0) {
				EndPanelLose.SetActive (true);
				EnabledPause ();
			}
		}
	}

    public void LoadGameScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SelectCamera()
    {
        if (buildingCamera != null)
        {
            buildingCamera.enabled = !buildingCamera.enabled;
            objectPlacement.enabled = buildingCamera.enabled;
            foreach (Behaviour childCompnent in buildingCamera.GetComponentsInChildren<Behaviour>())
                childCompnent.enabled = buildingCamera.enabled;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (buildingCamera.enabled)
                gameState = "building";
        }
        if (playerCamera != null)
        {
            playerCamera.enabled = !playerCamera.enabled;
            foreach (Behaviour childCompnent in player.GetComponentsInChildren<Behaviour>())
                childCompnent.enabled = playerCamera.enabled;

            Cursor.visible = false;
            if (playerCamera.enabled)
                gameState = "first person";
        }
    }

    public void ReadyState()
    {
        towerPlacementPanel.SetActive(false);
        fpsPanel.SetActive(true);
        objectPlacement.DesactivateScript();
        SelectCamera();
    }

	public void towerPlacementState()
	{
		towerPlacementPanel.SetActive(true);
		fpsPanel.SetActive(false);
		objectPlacement.ActivateScript ();
		SelectCamera();
	}

    public void EnabledPause()
    {
        firstPersonController.enabled = false;
        firstPersonShooting.enabled = false;
        moveCamera.enabled = false;
        fpsPanelScript.enabled = false;
        objectPlacement.DesactivateScript();
        Cursor.visible = true;
		Time.timeScale = 0f;
		pauseScript.canBeActivated = false;
		isInPause = true;
	}

    public void CursorVisible()
    {
        Cursor.visible = true;
    }


    public void DisabledPause()
    {
        if (gameState == "building")
        {
            moveCamera.enabled = true;
            objectPlacement.enabled = true;
            Cursor.visible = true;
        }
        else if (gameState == "first person")
        {
            firstPersonController.enabled = true;
            firstPersonShooting.enabled = true;
            fpsPanelScript.enabled = true;
            Cursor.visible = false;
        }
		Time.timeScale = 1.0f;
		pauseScript.canBeActivated = true;
		isInPause = false;
    }
}
