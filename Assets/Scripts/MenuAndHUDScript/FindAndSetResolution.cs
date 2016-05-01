using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FindAndSetResolution : MonoBehaviour {
    private Dropdown dropDown;
	// Use this for initialization
	void Start () {
        dropDown = GameObject.Find("Dropdown").GetComponent<Dropdown>();
        List<string> listOption = new List<string>();
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions) {
            print(res.width + "x" + res.height);
            listOption.Add(res.width + "x" + res.height);
        }
        
        listOption.Add("1920x1080");
        listOption.Add("1920x1080");
        listOption.Add("1920x1080");
        listOption.Add("1920x1080");
        
        dropDown.AddOptions(listOption);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    
    public void ChangeResolution(int num) {
        Resolution[] resolutions = Screen.resolutions;
        if (num < resolutions.Length) {
            Screen.SetResolution(resolutions[num].width, resolutions[num].height, true);            
        }
    }
}
