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
	private int selectedCastle = -1;
	
	public GameObject TutoLevel;
	
	private static string[] castleNames = {"West Castle Selected", "North Castle Selected", "East Castle Selected", "South Castle Selected"};
	//private string castleSpritesNames[4] = {"WorldMap1CastleUnlock", "WorldMap1CastleUnlock", "WorldMap1CastleUnlock", "WorldMap1CastleUnlock"};
	
	public bool objectAreInit;

	private void InitObjectsVariables() {
		listCastleButtonImage.Add(GameObject.Find("SelectWestCastleButton"));
		listCastleButtonImage.Add(GameObject.Find("SelectNorthCastleButton"));
		listCastleButtonImage.Add(GameObject.Find("SelectEastCastleButton"));
		listCastleButtonImage.Add(GameObject.Find("SelectSouthCastleButton"));
		textIndication = GameObject.Find("CastleSelectionTextIndication").GetComponent<Text>();
		worldMapImage = GameObject.Find("WorldMapImage").GetComponent<Image>();
		levelPlayButton = GameObject.Find("LevelSelectionPlayButton").GetComponent<Button>();
		objectAreInit = true;
	}

	public void InitBasicVariables() {
		if (objectAreInit) {
			DataContainerScript.instance.levelChoose = selectedCastle;
			nbCastleUnlock = DataContainerScript.instance.unlockCastle;
			selectedCastle = -1;
			Sprite sprt = Resources.Load<Sprite>("WorldMaps/WorldMap" + nbCastleUnlock + "CastleUnlock") as Sprite;
			worldMapImage.sprite = sprt;
			int i = 0;
			foreach (GameObject obj in listCastleButtonImage)
			{
				obj.GetComponent<ClickColliderLevel> ().unselectIt();
				if (i < nbCastleUnlock) {
					obj.SetActive (true);
				} else {
					obj.SetActive(false);
				}
				i++;
			}
			levelPlayButton.interactable = false;
			textIndication.text = "No Castle Selected";
		}
	}

	// Use this for initialization
	void Start () {
		InitObjectsVariables();
		InitBasicVariables();
	}

	// Update is called once per frame
	void Update () {	
		if (TutoLevel.activeSelf != DataContainerScript.instance.TutorialActivation) {
			TutoLevel.SetActive(DataContainerScript.instance.TutorialActivation);
		}
		
	
	}

	
	public void ChangeSelectedCastles(int num) {
		if (num >= 0 && num < nbCastleUnlock) {
			if (selectedCastle >= 0 && selectedCastle < nbCastleUnlock) {
				listCastleButtonImage [selectedCastle].GetComponent<ClickColliderLevel> ().unselectIt ();
			}
			selectedCastle = num;
			textIndication.text = castleNames[num];
			levelPlayButton.interactable = true;
		} else {
			selectedCastle = num;
			InitBasicVariables();
		}
	}
	
}
