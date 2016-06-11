using UnityEngine;
using System.Collections;

public class SellTowerButton : MonoBehaviour {

    private ObjectPlacement objectPlacement;

	void Start () {
        objectPlacement = GameObject.FindGameObjectWithTag("Map").GetComponent<ObjectPlacement>();
	}
	
	void Update () {
	
	}

    public void OnClick() {
        objectPlacement.SellTower();
    }


}
