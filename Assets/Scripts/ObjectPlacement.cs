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

    public float tileSize;
	[SerializeField] LevelManager manager;
	[SerializeField] PanelTowersManager towerManager;


    private GameObject selectionCube;
    private bool isMouseOverGUI = false;

    void Start()
    {
		manager = GameObject.Find("_SCRIPTS_").GetComponent<LevelManager>();
		towerManager = GameObject.Find("TowerPlacementTowerSelection").GetComponent<PanelTowersManager>();
		ActivateScript ();
    }

	public void ActivateScript()
	{
		selectionCube = (GameObject)Instantiate(selectionPrefab, buildingCamera.transform.position, gameObject.transform.rotation);
		this.enabled = true;
	}

    void Update()
    {
        Ray mapRay = buildingCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit mapHitInfo;
        RaycastHit objectHitInfo = new RaycastHit();
        bool canBuild = true;

        if (selectionCube == null)
            selectionCube = (GameObject)Instantiate(selectionPrefab, buildingCamera.transform.position, gameObject.transform.rotation);

        if (GetComponent<Collider>().Raycast(mapRay, out mapHitInfo, Mathf.Infinity))
        {
            //int x = Mathf.FloorToInt(mapHitInfo.point.x / tileSize);
            //int z = Mathf.FloorToInt(mapHitInfo.point.z / tileSize);

            currentTileCoord.x = mapHitInfo.point.x;
            currentTileCoord.y = mapHitInfo.point.y + 2f;
            currentTileCoord.z = mapHitInfo.point.z;

            selectionCube.transform.position = currentTileCoord/* * tileSize*/;

            Vector3 pointA = new Vector3(mapHitInfo.point.x, 1000f, mapHitInfo.point.z);

            Physics.SphereCast(pointA, 2.5f, Vector3.down, out objectHitInfo, Mathf.Infinity);

            if (objectHitInfo.collider.tag == "Object" || objectHitInfo.collider.tag == "Enemie" || objectHitInfo.collider.tag == "Tower")
            {
                selectionCube.GetComponent<MeshRenderer>().material = invalidMat;
                canBuild = false;
            }
            else if (objectHitInfo.collider.tag == "Map")
            {
                selectionCube.GetComponent<MeshRenderer>().material = validMat;
                canBuild = true;
            }

        }
        else
        {
            canBuild = false;
            selectionCube.GetComponent<MeshRenderer>().material = invalidMat;

        }

        if (!canBuild)
        {
            if (objectHitInfo.collider == null)
                return;
            if (Input.GetMouseButtonDown(1) && objectHitInfo.collider.tag == "Object")
            {
                Destroy(objectHitInfo.transform.parent.gameObject);
                Debug.Log("Destroy");
            }
            return;
        }

		int priceTower = towerManager.getCurrentlySelectedTowerPrice ();
        //Debug.Log (priceTower);
        //Debug.Log(manager.money);
        if (Input.GetMouseButtonDown(0) && !isMouseOverGUI && priceTower <= manager.money)
        {
            int indexTower = towerManager.getIdTowerCurrentlySelected();
            Instantiate(towers[indexTower], new Vector3(selectionCube.transform.position.x, selectionCube.transform.position.y - 2f, selectionCube.transform.position.z), selectionCube.transform.rotation);
			manager.money -= priceTower;
        }

    }

    public void DesactivateScript()
    {
        this.enabled = false;
        Destroy(selectionCube);
        selectionCube = null;
    }

    public void DisabledPlacement()
    {
        //Debug.Log("Mouse Over GUI");
        isMouseOverGUI = true;
    }

    public void EnabledPlacement()
    {
        isMouseOverGUI = false;
        //Debug.Log("Mouse Exit GUI");
    }
}