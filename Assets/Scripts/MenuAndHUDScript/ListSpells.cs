using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class ListSpells : MonoBehaviour {

	[SerializeField] GameObject buttonPreFab;
	[SerializeField] RectTransform content;
	[SerializeField] Image spellSelectedImage1;
	[SerializeField] Image spellSelectedImage2;
	[SerializeField] Image spellSelectedImage3;
	[SerializeField] Image spellSelectedImage4;

	int nbSelected = 0;
	int nbMax = 4;

	private static string[] spellsFiles = {"spell1", "spell2"};

	List<GameObject> buttonsList = new List<GameObject>();
	List<GameObject> listSpellsSelected = new List<GameObject>();

	List<SpellsInfos> listSpellsLoaded = new List<SpellsInfos>();

	// Use this for initialization
	void Start () {
		LoadSpellsList ();
		Vector3 vec =  new Vector3(50F, -50F, 0F);
		Vector3 secondX = new Vector3(600F, 0F, 0F);
		int line = 0;
		listSpellsLoaded.ForEach (delegate(SpellsInfos info) {
			GameObject button = (GameObject)Instantiate (buttonPreFab);
			button.transform.SetParent (content, false);
			if ((vec.y * -1) + 300 > content.sizeDelta.y) {
				
				float yDiff = ((vec.y * -1) + 300) - content.sizeDelta.y;
				content.sizeDelta += new Vector2 (0F, yDiff);
			}
			button.transform.localPosition = vec;
			vec += secondX;
			if (line == 1) {
				vec -= secondX * 2;
				vec += new Vector3 (0F, -350F, 0F);
				line = -1;
			}
			//content.sizeDelta += new Vector2 (0F, 200F);
			button.transform.Find ("SpellsImage").GetComponentInChildren<Image> ().sprite = Resources.Load<Sprite> (info.imageResourcePath) as Sprite;
			button.transform.Find ("SpellsName").GetComponentInChildren<Text> ().text = info.name;
			button.transform.Find ("SpellsDescription").GetComponentInChildren<Text> ().text = info.description;
			buttonsList.Add (button);
			line += 1;
		});
		content.transform.localPosition = new Vector3(0F, 0F, 0F);

		buttonsList.ForEach (delegate(GameObject but) {
			but.GetComponentInChildren<Toggle> ().onValueChanged.AddListener ((on) => {
				if (on) {
					nbSelected++;
					listSpellsSelected.Add(but);
				} else {
					nbSelected--;
					listSpellsSelected.Remove(but);
				}
				if (nbSelected == nbMax) {
					buttonsList.ForEach (delegate(GameObject but2) {
						if(but2.GetComponentInChildren<Toggle> ().isOn == false) {
							but2.GetComponentInChildren<Toggle> ().interactable = false;
						} else {
							but2.GetComponentInChildren<Toggle> ().interactable = true;
						}
					});
				} else {
					buttonsList.ForEach (delegate(GameObject but2) {
						but2.GetComponentInChildren<Toggle> ().interactable = true;
					});
				}
				updateImages();
			});
		});
			
	}

	private void updateImages () {
		spellSelectedImage1.enabled = false;
		spellSelectedImage2.enabled = false;
		spellSelectedImage3.enabled = false;
		spellSelectedImage4.enabled = false;
		if (listSpellsSelected.Count() > 0) {
			spellSelectedImage1.sprite = listSpellsSelected [0].transform.Find("SpellsImage").GetComponentInChildren<Image> ().sprite;
			spellSelectedImage1.enabled = true;
		}
		if (listSpellsSelected.Count() > 1) {
			spellSelectedImage2.sprite = listSpellsSelected [1].transform.Find("SpellsImage").GetComponentInChildren<Image> ().sprite;
			spellSelectedImage2.enabled = true;
		}
		if (listSpellsSelected.Count() > 2) {
			spellSelectedImage3.sprite = listSpellsSelected [2].transform.Find("SpellsImage").GetComponentInChildren<Image> ().sprite;
			spellSelectedImage3.enabled = true;
		}
		if (listSpellsSelected.Count() > 3) {
			spellSelectedImage4.sprite = listSpellsSelected [3].transform.Find("SpellsImage").GetComponentInChildren<Image> ().sprite;
			spellSelectedImage4.enabled = true;
		}
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	private void LoadSpellsList () {
		for (int i = 0; i < spellsFiles.Count(); i++) {
			TextAsset file = Resources.Load("SpellsInfos/" + spellsFiles [i]) as TextAsset;
		
			string jsonText = file.text;
			SpellsInfos infos = JsonUtility.FromJson<SpellsInfos>(jsonText);
			listSpellsLoaded.Add (infos);
		}
	
	}

    public void LoadGameScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}


[Serializable]
public class SpellsInfos
{
	public string imageResourcePath;
	public string name;
	public string description;
}