using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class DataContainerScript {

	public bool TutorialActivation = false;
	public int levelChoose = -1;
	public int unlockCastle = 2;
	public List<SpellsInfos> listSpellsSelected = new List<SpellsInfos>();

	 static private DataContainerScript _instance;
     static public DataContainerScript instance {
         get {
             if(_instance == null)
                 _instance = new DataContainerScript();
             return _instance;
         }
	 }

	 protected DataContainerScript () {
		 /*if (File.Exists(Application.persistentDataPath + "/save.json")) {
			string jsonText = "{}";
			FileStream fs = File.Open(Application.persistentDataPath + "/save.json", FileMode.Open);
			if (fs != null) {
				StreamReader reader = new StreamReader(fs);
				jsonText = reader.ReadToEnd();
				reader.Close();
				SaveDatas infos = JsonUtility.FromJson<SaveDatas>(jsonText);
				if (infos != null) {
					TutorialActivation = infos.Tutorial == 0 ? false : true;	
					unlockCastle = infos.UnlockLevels;
				}
			}
		}*/
	 }

	public void AddSpellList(List<SpellsInfos> newlistSpells) {
		listSpellsSelected = newlistSpells;
	}

	private void saveCurrentSettings() {
		SaveDatas infos = new SaveDatas();
		infos.Tutorial = TutorialActivation ? 1 : 0;
		infos.UnlockLevels = unlockCastle;
		string json = JsonUtility.ToJson(infos);

		FileStream fs = File.Open(Application.persistentDataPath + "/save.json", FileMode.Create);
		if (fs != null) {
			StreamWriter writer = new StreamWriter(fs);
			writer.Write(json);
			writer.Close();
		}
		
		
	}

	public void SetTutorialActivation(bool isActivate) {
		Debug.Log(isActivate);
		TutorialActivation = isActivate;
		saveCurrentSettings();
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

[Serializable]
public class SaveDatas
{
	public int UnlockLevels;
	public int Tutorial;
}