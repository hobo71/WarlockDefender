using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Camera buildingCamera;
    public Camera playerCamera;
    public GameObject player;
    public GameObject map;
    public GameObject fpsPanel;
    public GameObject crosshair;
    public GameObject towerPlacementPanel;
	public int money;
	public int waveNb;

    private ObjectPlacement objectPlacement;
    private FirstPersonController firstPersonController;
    private FirstPersonShooting firstPersonShooting;
    private MoveCamera moveCamera;
    private FPSPanelScript fpsPanelScript;

    private string gameState = "building";

    void Start () {
        objectPlacement = map.GetComponent<ObjectPlacement>();
        firstPersonController = player.GetComponent<FirstPersonController>();
        firstPersonShooting = player.GetComponent<FirstPersonShooting>();
        moveCamera = buildingCamera.GetComponent<MoveCamera>();
        fpsPanelScript = fpsPanel.GetComponent<FPSPanelScript>();
		money = 5000;
		waveNb = 1;
    }
	
	void Update () {
        if (Input.GetKeyDown("c"))
            SelectCamera();
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
            crosshair.SetActive(playerCamera.enabled);
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

    public void EnabledPause()
    {
        firstPersonController.enabled = false;
        firstPersonShooting.enabled = false;
        moveCamera.enabled = false;
        fpsPanelScript.enabled = false;
        objectPlacement.DesactivateScript();
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
    }
}
