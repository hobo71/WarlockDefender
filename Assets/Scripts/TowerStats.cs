using UnityEngine;
using System.Collections;

public class TowerStats : MonoBehaviour {

    [SerializeField]
    GameObject towerInfoPanel;

    public GameObject projectilePrefab;
    public GameObject projectilePrefabLvl2;
    public Sprite towerImage;
    public string towerName = "Tower";
    public float towerDamage = 10f;
    public float attackSpeed = 0.5f;
    public float projectileDamage = 10f;
    public float projectileSpeed = 15f;
    public float attackRange = 10f;
    public float spreadZone = 0f;
    public float towerPrice = 100f;
    public float sellPercentage = 20f;
    public float towerPriceUp;
    public int lvl = 1;
    public bool isUpdate = false;
    private GameObject infoPanel;

    private LevelManager manager;

    void Start () {
        manager = GameObject.Find("_SCRIPTS_").GetComponent<LevelManager>();
        towerPriceUp = towerPrice * 5;
	}
	
	void Update () {
	
	}

    public GameObject ShowTowerInfo() {
        Debug.Log("Show Info Panel");
        if (infoPanel != null)
            Destroy(infoPanel);
        infoPanel = Instantiate(towerInfoPanel);
        infoPanel.transform.SetParent(GameObject.Find("GameHUDCanvas").transform, false);
        FillTowerInfoPanel fillTowerInfo = infoPanel.GetComponent<FillTowerInfoPanel>();
        fillTowerInfo.SetTowerImageInfo(towerImage);
        fillTowerInfo.SetTowerTypeInfo(towerName);
        fillTowerInfo.SetTowerLevel("Level : " + lvl.ToString());
        fillTowerInfo.SetTowerPrice("Price for up : " + towerPriceUp.ToString());
        fillTowerInfo.SetTowerDamage("Damage : " + projectileDamage.ToString());
        var DamageSec = projectileDamage / attackSpeed;
        fillTowerInfo.SetTowerAttackSpeed("Damage/S : " + DamageSec.ToString("F1"));
        float upDamage = projectileDamage + 10.0f;
        fillTowerInfo.SetTowerUpDamage("Up Damage : " + upDamage.ToString());
        float upSpeed = upDamage / (attackSpeed - 0.1f);
        fillTowerInfo.SetTowerUpAttackSpeed("Up Damage/S : " + upSpeed.ToString("F1"));
        if (isUpdate)
            fillTowerInfo.SetButtonActive(false);        
        return infoPanel;
    }

}
