using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FillTowerInfoPanel : MonoBehaviour {

    [SerializeField]
    GameObject towerImage;
    [SerializeField]
    GameObject towerType;

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

    public void SetOnInfoPanel(bool state) {
        onInfoPanel = state;
    }

    public bool GetOnInfoPanel() {
        return onInfoPanel;
    }
}
