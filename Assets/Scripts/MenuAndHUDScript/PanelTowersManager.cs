using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelTowersManager : MonoBehaviour {

	public GameObject panelTowers;
	[SerializeField] GameObject towerButtonPreFab;
	[SerializeField] LevelManager manager;
	[SerializeField] Sprite[] towersSprite;
	[SerializeField] int[] towersPrice;

	private List<GameObject> listButtonCreated = new List<GameObject>();

	private float marge = 30;
	int idTowerCurrentlySelected;
    private bool isTowerSelected = false;

	// Use this for initialization
	void Start () {
        towerButtonPreFab.SetActive (true);
        isTowerSelected = false;
		Vector3 vecDefault = towerButtonPreFab.GetComponentInChildren<RectTransform> ().anchoredPosition;
		float sizeCase = towerButtonPreFab.GetComponentInChildren<RectTransform> ().sizeDelta.x;
		vecDefault += new Vector3 (marge, 0F, 0F);
		int i = 0;
		for (i = 0; i < towersSprite.Length; i++) {
			GameObject button = Instantiate (towerButtonPreFab);
			button.GetComponentInChildren<Toggle> ().isOn = false;
			RectTransform transf = button.GetComponentInChildren<RectTransform> ();
			transf.SetParent (panelTowers.transform, false);
			transf.anchoredPosition = vecDefault;
			vecDefault += new Vector3 (transf.sizeDelta.x + marge, 0F, 0F);
			button.GetComponentInChildren<Toggle> ().group = panelTowers.GetComponentInChildren<ToggleGroup> ();
			button.GetComponentInChildren<Image> ().sprite = towersSprite[i];
			button.GetComponentInChildren<Text> ().text = "" + towersPrice[i];
			int idButton = i;
			button.GetComponentInChildren<Toggle> ().onValueChanged.AddListener ((on) => {
				if (on) {
                    idTowerCurrentlySelected = idButton;
                    isTowerSelected = true;
                }
                else {
                    isTowerSelected = false;
                }
            });
			if (i == 0) {
				button.GetComponentInChildren<Toggle> ().isOn = true;
			}
			listButtonCreated.Add (button);
		}
		if (i > 2) {
			RectTransform panelRect = panelTowers.GetComponent<RectTransform> ();
			panelRect.sizeDelta = new Vector2 (i * (sizeCase + marge) + 60 + marge, panelRect.sizeDelta.y);
		}
		towerButtonPreFab.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		int i = 0;
		for (i = 0; i < towersPrice.Length; i++) {
			if (towersPrice [i] > manager.money) {
				listButtonCreated [i].GetComponentInChildren<Toggle> ().isOn = false;
				listButtonCreated [i].GetComponentInChildren<Toggle> ().interactable = false;
            }
            else {
				listButtonCreated [i].GetComponentInChildren<Toggle> ().interactable = true;
			}
		}
	}

	public int getIdTowerCurrentlySelected () {
		return idTowerCurrentlySelected;
	}


	public int getCurrentlySelectedTowerPrice () {
		return towersPrice [idTowerCurrentlySelected];
	}

    public bool GetSelectionStatus() {
        return isTowerSelected;
    }

    public void DeselectTower() {
        listButtonCreated[idTowerCurrentlySelected].GetComponentInChildren<Toggle>().isOn = false;
    }
}
