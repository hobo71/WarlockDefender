using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDInfosDisplayed : MonoBehaviour {

	[SerializeField] Text waveText;
	[SerializeField] Text moneyText;
	[SerializeField] LevelManager manager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "" + manager.money;
		waveText.text = "" + manager.waveNb;
	}
}
