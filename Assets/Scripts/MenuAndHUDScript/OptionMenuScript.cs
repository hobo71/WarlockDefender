using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OptionMenuScript : MonoBehaviour {
	public Dropdown dropDown;
	
	public Toggle tutoToggle;
	// Use this for initialization
	void Start () {
        List<string> listOption = new List<string>();
        Resolution[] resolutions = Screen.resolutions;
		string currentRes = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
		int resPosition = 0;
		int i = 0;
        foreach (Resolution res in resolutions) {
            string resString = res.width + "x" + res.height;
			listOption.Add(resString);
			if (resString == currentRes) {
				resPosition = i;
			}
			i++;
        }
        dropDown.AddOptions(listOption);
		dropDown.value = resPosition;
		tutoToggle.isOn = DataContainerScript.instance.TutorialActivation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
	public void ChangeTutorialActivation(bool isActivate) {
		DataContainerScript.instance.SetTutorialActivation(isActivate);
	}
	
	public void ClearSave() {
		DataContainerScript.instance.RelockAll();
	}
    
	public void CheatUnlock() {
		DataContainerScript.instance.UnlockAll();
	}
	
    public void ChangeResolution(int num) {
        Resolution[] resolutions = Screen.resolutions;
        if (num < resolutions.Length) {
            Screen.SetResolution(resolutions[num].width, resolutions[num].height, true);            
        }
    }
}
