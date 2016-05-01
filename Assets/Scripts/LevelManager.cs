using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Camera buildingCamera;
    public Camera playerCamera;

    public GameObject player;
    public GameObject map;

    public GameObject crosshair;

    private ObjectPlacement objectPlacement;

    void Start () {
        objectPlacement = map.GetComponent<ObjectPlacement>();
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
            CusorIsVisible(true);
        }
        if (playerCamera != null)
        {
            playerCamera.enabled = !playerCamera.enabled;
            crosshair.SetActive(playerCamera.enabled);
            foreach (Behaviour childCompnent in player.GetComponentsInChildren<Behaviour>())
                childCompnent.enabled = playerCamera.enabled;

            //Cursor.lockState = CursorLockMode.Locked;
            CusorIsVisible(false);
        }
    }

    public void CusorIsVisible(bool visible)
    {
        Cursor.visible = visible;
    }
}
