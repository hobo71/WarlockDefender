using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ObjectPlacement : MonoBehaviour
{

    Vector3 currentTileCoord;

    public Material validMat;
    public Material invalidMat;

    public GameObject selectionPrefab;
    public GameObject[] towers;

    public Camera buildingCamera;

	[SerializeField] LevelManager manager;
	[SerializeField] PanelTowersManager towerManager;
    [SerializeField]
    GameObject currentInfoPanel = null;
    TowerStats currentTowerStats = null;


    private GameObject selectionCube;
    private bool isMouseOverGUI = false;

    void Start() {
		manager = GameObject.Find("_SCRIPTS_").GetComponent<LevelManager>();
		towerManager = GameObject.Find("TowerPlacementTowerSelection").GetComponent<PanelTowersManager>();
		ActivateScript ();
    }

	public void ActivateScript() {
		selectionCube = (GameObject)Instantiate(selectionPrefab, buildingCamera.transform.position, gameObject.transform.rotation);
		this.enabled = true;
	}

    void Update() {
        if (!towerManager.GetSelectionStatus())
        {
            if (Input.GetMouseButtonDown(0) && !isMouseOverGUI) {
                Ray ray = buildingCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
                    if (hitInfo.collider.tag == "Tower") {
                        if (currentInfoPanel) {
                            Destroy(currentInfoPanel);
                            currentInfoPanel = null;
                            currentTowerStats = null;
                        }
                        currentTowerStats = hitInfo.collider.GetComponentInParent<TowerStats>();
                        currentInfoPanel = currentTowerStats.ShowTowerInfo();
                    }
                    else {
                        if (currentInfoPanel) {
                            FillTowerInfoPanel panelScript = currentInfoPanel.GetComponent<FillTowerInfoPanel>();
                            if (!panelScript.GetOnInfoPanel()) {
                                Destroy(currentInfoPanel);
                                currentInfoPanel = null;
                                currentTowerStats = null;
                            }
                        }
                    }
                }
            }
            return;
        }

        Ray mapRay = buildingCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mapHitInfo;
        RaycastHit objectHitInfo = new RaycastHit();

        if (selectionCube == null)
            selectionCube = (GameObject)Instantiate(selectionPrefab, buildingCamera.transform.position, gameObject.transform.rotation);

        if (GetComponent<Collider>().Raycast(mapRay, out mapHitInfo, Mathf.Infinity)) {
            currentTileCoord.x = mapHitInfo.point.x;
            currentTileCoord.y = mapHitInfo.point.y + 2f;
            currentTileCoord.z = mapHitInfo.point.z;

            selectionCube.transform.position = currentTileCoord;

            Vector3 pointA = new Vector3(mapHitInfo.point.x, 1000f, mapHitInfo.point.z);

            Physics.SphereCast(pointA, 2.5f, Vector3.down, out objectHitInfo, Mathf.Infinity);

            if (objectHitInfo.collider.tag == "Object" || objectHitInfo.collider.tag == "Enemie" || objectHitInfo.collider.tag == "Tower") {
                selectionCube.GetComponent<MeshRenderer>().material = invalidMat;
            }
            else if (objectHitInfo.collider.tag == "Map") {
                selectionCube.GetComponent<MeshRenderer>().material = validMat;
            }

        }
        else {
            selectionCube.GetComponent<MeshRenderer>().material = invalidMat;

        }

		int priceTower = towerManager.getCurrentlySelectedTowerPrice ();

        if (Input.GetMouseButtonDown(0) && !isMouseOverGUI && priceTower <= manager.money) {
            int indexTower = towerManager.getIdTowerCurrentlySelected();
            Instantiate(towers[indexTower], new Vector3(selectionCube.transform.position.x, selectionCube.transform.position.y - 2f, selectionCube.transform.position.z), selectionCube.transform.rotation);
			manager.money -= priceTower;
            towerManager.DeselectTower();
            Destroy(selectionCube);
        }

    }

    public void SellTower() {
        if (!currentTowerStats || !currentInfoPanel)
            return;

        GameObject tower = currentTowerStats.transform.root.gameObject;
        manager.money += Mathf.RoundToInt((currentTowerStats.towerPrice * currentTowerStats.sellPercentage) / 100);

        Destroy(tower);
        currentTowerStats = null;
        Destroy(currentInfoPanel);
        currentInfoPanel = null;
    }

    public void DesactivateScript() {
        this.enabled = false;
        Destroy(selectionCube);
        selectionCube = null;
        currentTowerStats = null;
        Destroy(currentInfoPanel);
        currentInfoPanel = null;
    }

    public void DisabledPlacement() {
		Debug.Log ("plop");
        isMouseOverGUI = true;
    }

    public void EnabledPlacement() {
        isMouseOverGUI = false;
    }
}