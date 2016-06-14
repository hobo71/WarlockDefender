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
    public GameObject[] traps;
    public GameObject TowerBlipPrefab;
    public GameObject cameraTexture;

    public Camera buildingCamera;

    [SerializeField] LevelManager manager;
	[SerializeField] PanelTowersManager towerManager;
    [SerializeField]
    GameObject currentInfoPanel = null;
    TowerStats currentTowerStats = null;

    private GameObject selectionCube;
    private bool isMouseOverGUI = false;

    [SerializeField] Terrain terrain;
    [SerializeField] int roadTextureId = 0;

    bool canPlace = false;
    //bool isRoad = false;

    void Start() {
		manager = GameObject.Find("_SCRIPTS_").GetComponent<LevelManager>();
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

            int indexTower = towerManager.getIdTowerCurrentlySelected();
            int price = towerManager.getCurrentlySelectedTowerPrice();
            if (objectHitInfo.collider.tag == "Object" || objectHitInfo.collider.tag == "Enemie" || objectHitInfo.collider.tag == "Tower" ||
                price > manager.money || IsRoadMainTexture() == (indexTower < towers.Length))
            {
                    canPlace = false;
                    selectionCube.GetComponent<MeshRenderer>().material = invalidMat;
            }
            else if (objectHitInfo.collider.tag == "Map") {
                canPlace = true;
                selectionCube.GetComponent<MeshRenderer>().material = validMat;
            }

        }
        else {
            canPlace = false;
            selectionCube.GetComponent<MeshRenderer>().material = invalidMat;
        }

		int priceTower = towerManager.getCurrentlySelectedTowerPrice ();

        if (Input.GetMouseButtonDown(0) && canPlace && !isMouseOverGUI && priceTower <= manager.money)
        {
            GameObject placedTower = null;
            int indexTower = towerManager.getIdTowerCurrentlySelected();
            var position = new Vector3(selectionCube.transform.position.x, selectionCube.transform.position.y - 2f, selectionCube.transform.position.z);
            if (indexTower < towers.Length)
                placedTower = (GameObject)Instantiate(towers[indexTower], position, selectionCube.transform.rotation);
            else
                placedTower = (GameObject)Instantiate(traps[indexTower - towers.Length], position, selectionCube.transform.rotation);
            GameObject towerBlip = Instantiate(TowerBlipPrefab);
            towerBlip.transform.SetParent(cameraTexture.transform, false);
            BlipScript scriptBlip = towerBlip.GetComponent<BlipScript>();
            scriptBlip.target = placedTower.transform;
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
        isMouseOverGUI = true;
    }

    public void EnabledPlacement() {
        isMouseOverGUI = false;
    }
    
    float[] GetTextureMix(Vector3 position)
    {
        var mapX = ((position.x - terrain.transform.position.x) / terrain.terrainData.size.x) * terrain.terrainData.alphamapWidth;
        var mapZ = ((position.z - terrain.transform.position.z) / terrain.terrainData.size.z) * terrain.terrainData.alphamapHeight;

        var splatData = terrain.terrainData.GetAlphamaps((int)mapX, (int)mapZ, 1, 1);
        float[] cellMix = new float[splatData.GetUpperBound(2) + 1];
        for (var i = 0; i<cellMix.Length; i++)
        {
            cellMix[i] = splatData[0, 0, i];
        }
        return cellMix;
    }

    bool IsRoadMainTexture()
    {
        float[] mix = GetTextureMix(selectionCube.transform.position);
        float roadMix = mix[roadTextureId];

        for (var i = 0; i<mix.Length; i++)
            if (mix[i] > roadMix)
                return false;
        //isRoad = true;
        return true;
    }

  public void UpdateTower(GameObject button)
    {
        if (currentTowerStats == null)
            return;
        GameObject tower = currentTowerStats.transform.root.gameObject;

        if (manager.money >= currentTowerStats.towerPrice)
        {
            currentTowerStats.isUpdate = true;
            currentTowerStats.projectileDamage += 10;
            currentTowerStats.attackSpeed -= 0.1f;
            currentTowerStats.lvl += 1;
            currentTowerStats.ShowTowerInfo();
            currentTowerStats.projectilePrefab = currentTowerStats.projectilePrefabLvl2;
            manager.money -= (int)currentTowerStats.towerPriceUp;
            button.SetActive(false);
        }
    }

}