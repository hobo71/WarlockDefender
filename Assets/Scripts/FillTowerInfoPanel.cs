using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillTowerInfoPanel : MonoBehaviour {

    [SerializeField]
    GameObject towerImage;
    [SerializeField]
    GameObject towerType;
[SerializeField]
    GameObject towerLevel;
    [SerializeField]
    GameObject towerPrice;
    [SerializeField]
    GameObject towerDamage;
    [SerializeField]
    GameObject towerAttackSpeed;
    [SerializeField]
    GameObject towerUpDamage;
    [SerializeField]
    GameObject towerUpAttackSpeed;
    [SerializeField]
    GameObject towerUp;
    private bool onInfoPanel = false;

    void Start () {
	
	}
	
	void Update () {
	
	}

    public void SetTowerTypeInfo(string type) {
        towerType.GetComponent<Text>().text = type;
    }

    public void SetTowerImageInfo(Sprite image) {
        towerImage.GetComponent<Image>().sprite = image;
    }

    public void SetTowerLevel(string level)
    {
        towerLevel.GetComponent<Text>().text = level;
    }

    public void SetTowerPrice(string price)
    {
        towerPrice.GetComponent<Text>().text = price;
    }

    public void SetOnInfoPanel(bool state) {
        onInfoPanel = state;
    }

    public bool GetOnInfoPanel() {
        return onInfoPanel;
    }

    public void SetTowerDamage(string damage)
    {
        towerDamage.GetComponent<Text>().text = damage;
    }

    public void SetTowerAttackSpeed(string attSpeed)
    {
        towerAttackSpeed.GetComponent<Text>().text = attSpeed;
    }
    public void SetTowerUpDamage(string damage)
    {
        towerUpDamage.GetComponent<Text>().text = damage;
        towerUpDamage.GetComponent<Text>().color = Color.green;
    }

    public void SetTowerUpAttackSpeed(string attSpeed)
    {
        towerUpAttackSpeed.GetComponent<Text>().text = attSpeed;
        towerUpAttackSpeed.GetComponent<Text>().color = Color.green;
    }

    public void SetButtonActive(bool isActive)
    {
        towerUp.SetActive(isActive);
        towerUpAttackSpeed.SetActive(isActive);
        towerUpDamage.SetActive(isActive);
    }
}
