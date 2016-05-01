using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSPanelScript : MonoBehaviour {

	public GameObject lifeBar;
	public GameObject Spell1;
	public GameObject Spell2;
	public GameObject Spell3;
	public GameObject Spell4;
    public PlayerStats playerStats;

	private float life = 100f;

    private RectTransform transf;

    // Use this for initialization
    void Start () {
        transf = lifeBar.GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			Spell1.GetComponent<Toggle> ().isOn = true;
		}
		if (Input.GetKeyDown ("2")) {
			Spell2.GetComponent<Toggle> ().isOn = true;
		}
		if (Input.GetKeyDown ("3")) {
			Spell3.GetComponent<Toggle> ().isOn = true;
		}
		if (Input.GetKeyDown ("4")) {
			Spell4.GetComponent<Toggle> ().isOn = true;
		}

        life = playerStats.life;
        transf.sizeDelta = new Vector2(400F * life / 100F, 30F);

    }
}
