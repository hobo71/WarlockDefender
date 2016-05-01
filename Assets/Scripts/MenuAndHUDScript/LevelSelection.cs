using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelection : MonoBehaviour {

    private List<GameObject> listCastleButtonImage = new List<GameObject>();
	private Image worldMapImage;
	private Button levelPlayButton;
	private Text textIndication;
		
	private int nbCastleUnlock;
	private int selectedCastle;
	
	private static string[] castleNames = {"West Castle Selected", "North Castle Selected", "East Castle Selected", "South Castle Selected"};
	//private string castleSpritesNames[4] = {"WorldMap1CastleUnlock", "WorldMap1CastleUnlock", "WorldMap1CastleUnlock", "WorldMap1CastleUnlock"};

	private void InitObjectsVariables() {
		listCastleButtonImage.Add(GameObject.Find("SelectWestCastleButton"));
		listCastleButtonImage.Add(GameObject.Find("SelectNorthCastleButton"));
		listCastleButtonImage.Add(GameObject.Find("SelectEastCastleButton"));
		listCastleButtonImage.Add(GameObject.Find("SelectSouthCastleButton"));
		textIndication = GameObject.Find("CastleSelectionTextIndication").GetComponent<Text>();
		worldMapImage = GameObject.Find("WorldMapImage").GetComponent<Image>();
		levelPlayButton = GameObject.Find("LevelSelectionPlayButton").GetComponent<Button>();
	}

	public void InitBasicVariables() {
		nbCastleUnlock = 3;
		selectedCastle = -1;
		Sprite sprt = Resources.Load<Sprite>("WorldMaps/WorldMap" + nbCastleUnlock + "CastleUnlock") as Sprite;
		worldMapImage.sprite = sprt;
		int i = 0;
		foreach (GameObject obj in listCastleButtonImage)
		{
			obj.GetComponent<Button>().interactable = true;
			if (i < nbCastleUnlock) {
				obj.GetComponent<Image>().enabled = true;		
			} else {
				obj.GetComponent<Image>().enabled = false;
			}
			i++;
		}
		levelPlayButton.interactable = false;
		textIndication.text = "No Castle Selected";
	}

	// Use this for initialization
	void Start () {
		InitObjectsVariables();
		InitBasicVariables();
	}

	// Update is called once per frame
	void Update () {
	
		
	
	}

	
	public void ChangeSelectedCastles(int num) {
		selectedCastle = num;
		if (num >= 0 && num < nbCastleUnlock) {
			textIndication.text = castleNames[num];
			levelPlayButton.interactable = true;			
		} else {
			InitBasicVariables();
		}
	}
	
}
