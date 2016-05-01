using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayPanelTowers : MonoBehaviour {

	public GameObject panelTowers;
	[SerializeField] GameObject towerButtonPreFab;
	private float marge = 30;

	// Use this for initialization
	void Start () {
		Vector3 vecDefault = towerButtonPreFab.GetComponentInChildren<RectTransform> ().anchoredPosition;
		float sizeCase = towerButtonPreFab.GetComponentInChildren<RectTransform> ().sizeDelta.x;
		vecDefault += new Vector3 (marge, 0F, 0F);
		int i = 0;
		for (i = 0; i < 5; i++) {
			GameObject button = (GameObject)Instantiate (towerButtonPreFab);
			button.GetComponentInChildren<Toggle> ().isOn = false;
			RectTransform transf = button.GetComponentInChildren<RectTransform> ();
			transf.SetParent (panelTowers.transform, false);
			transf.anchoredPosition = vecDefault;
			vecDefault += new Vector3 (transf.sizeDelta.x + marge, 0F, 0F);
			button.GetComponentInChildren<Toggle> ().group = panelTowers.GetComponentInChildren<ToggleGroup> ();
			if (i == 0) {
				button.GetComponentInChildren<Toggle> ().isOn = true;
			}
		}
		if (i > 2) {
			RectTransform panelRect = panelTowers.GetComponent<RectTransform> ();
			panelRect.sizeDelta = new Vector2 (i * (sizeCase + marge) + 60 + marge, panelRect.sizeDelta.y);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
