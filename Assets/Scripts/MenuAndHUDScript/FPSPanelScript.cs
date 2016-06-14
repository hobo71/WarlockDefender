using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class FPSPanelScript : MonoBehaviour {

	public GameObject TextTuto;
	public GameObject lifeBar;
	public FirstPersonShooting ShootingManager;
	public GameObject[] Spells;
    public PlayerStats playerStats;


	public bool tuto;
	private float life = 100f;
	private List<SpellsInfos> listSpells;
	private int listSize = 0;
    private RectTransform transf;
	private float[] currentCoolDown = new float[4];


	private int currentIdSpellOn = -1;

	//private int stateOfTuto = 0;

    // Use this for initialization
    void Start () {
        transf = lifeBar.GetComponent<RectTransform> ();
		listSpells = DataContainerScript.instance.listSpellsSelected;
		listSize = listSpells.Count ();
		tuto = DataContainerScript.instance.TutorialActivation;
		for (int i = 0; i < Spells.Length; i++) {
			if (listSize > i) {
				Spells[i].transform.FindChild("Background").GetComponent<Image> ().sprite = Resources.Load<Sprite> (listSpells [i].imageResourcePath) as Sprite;
				Spells[i].transform.FindChild ("SpellMask").GetComponent<RectTransform> ().localScale = new Vector3 (1, 0, 1);
				int idSpell = listSpells [i].id;
				Spells [i].GetComponent<Toggle> ().onValueChanged.AddListener((on) => {
					if (on) {
						ShootingManager.newSpellIsSelected(idSpell);
					}
				});
				currentCoolDown [i] = 0;
			}
		}
		if (tuto) {
			TextTuto.SetActive(true);
		}
	}
	
	public void removeTuto() {
		if (tuto) {
			TextTuto.SetActive(false);
			DataContainerScript.instance.SetTutorialActivation(false);
			tuto = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Spells.Length; i++) {
			if (Input.GetKeyDown ("" + (i + 1)) && listSize > i && currentCoolDown [i] == 0) {
				Spells [i].GetComponent<Toggle> ().isOn = true;
				currentIdSpellOn = i;
			}
			if (currentCoolDown [i] != 0) {
				currentCoolDown [i] -= Time.deltaTime;
				if (currentCoolDown [i] < 0) {
					currentCoolDown [i] = 0;
				}
				float scaleUpSpellMask = currentCoolDown [i] / listSpells [i].coolDown;
				Spells [i].transform.FindChild ("SpellMask").GetComponent<RectTransform> ().localScale = new Vector3 (1.0f, scaleUpSpellMask, 1.0f);
			}
		}



        life = playerStats.GetCurrentLife();
        transf.sizeDelta = new Vector2(400F * life / 100F, 30F);

    }

	public void StartCoolDownCurrentSpell() {
		if (currentIdSpellOn != -1) {
			currentCoolDown [currentIdSpellOn] = listSpells [currentIdSpellOn].coolDown;
			Spells[currentIdSpellOn].transform.FindChild ("SpellMask").GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 1);
		}
	}

	public void TurnAllSpellsToFalse() {
		ShootingManager.RemoveCurrentArea();
		for (int i = 0; i < Spells.Length; i++) {
			Spells[i].GetComponent<Toggle> ().isOn = false;
		}
		currentIdSpellOn = -1;
	}
}
