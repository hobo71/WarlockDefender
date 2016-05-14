using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DataContainerScript : MonoBehaviour {

	public bool TutorialActivation = true;
	public int levelChoose = -1;
	public List<SpellsInfos> listSpellsSelected = new List<SpellsInfos>();


	void Start () {
		UnityEngine.Object.DontDestroyOnLoad (gameObject);
		if (GameObject.FindGameObjectsWithTag("MenuDatas").Length > 1)
		{
			Destroy(gameObject);
		}
	}

	void Update () {}

	public void AddSpellList(List<SpellsInfos> newlistSpells) {
		listSpellsSelected = newlistSpells;
	}

	public void SetTutorialActivation(bool isActivate) {
		TutorialActivation = isActivate;
	}
}


[Serializable]
public class SpellsInfos
{
	public int id;
	public string imageResourcePath;
	public string name;
	public string description;
	public float coolDown;
}