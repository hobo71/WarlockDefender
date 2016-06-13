using UnityEngine;
using System.Collections;

public class UpgradeTower : MonoBehaviour {

	private ObjectPlacement objectPlacement;

	// Use this for initialization
	void Start () {
		objectPlacement = GameObject.FindGameObjectWithTag("Map").GetComponent<ObjectPlacement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick(GameObject button)
	{
		objectPlacement.UpdateTower(button);
	}
}
