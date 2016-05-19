using UnityEngine;
using System.Collections;

public class TowerStats : MonoBehaviour {

    [SerializeField]
    GameObject towerInfoPanel;

    public GameObject projectilePrefab;
    public Sprite towerImage;
    public string towerName = "Tower";
    public float towerDamage = 10f;
    public float attackSpeed = 0.5f;
    public float projectileDamage = 10f;
    public float projectileSpeed = 15f;
    public float attackRange = 10f;
    public float spreadZone = 0f;
    public float sellPercentage = 20f;

    void Start () {

	}
	
	void Update () {
	
	}

    public GameObject ShowTowerInfo() {
        Debug.Log("Show Info Panel");
        GameObject infoPanel = Instantiate(towerInfoPanel);
        infoPanel.transform.SetParent(GameObject.Find("GameHUDCanvas").transform, false);
        FillTowerInfoPanel fillTowerInfo = infoPanel.GetComponent<FillTowerInfoPanel>();
        fillTowerInfo.SetTowerImageInfo(towerImage);
        fillTowerInfo.SetTowerTypeInfo(towerName);
        return infoPanel;
    }
}
