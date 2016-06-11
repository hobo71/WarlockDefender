using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickColliderLevel : MonoBehaviour
{
	public LevelSelection levelSelectionScript;
	public int idSelectionCastle;

	private Color basicColor = new Color(1, 1, 1, 0);
	private Color hoverColor = new Color(1, 1, 1, 1);

	private bool inTheCollider = false;
	private bool isSelected = false;

	void Start () {

	}

	void Update () {
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		if (gameObject.GetComponent<PolygonCollider2D> ().OverlapPoint (mousePosition)) {
			// mouse in the collider
			if (inTheCollider == false) {
				if (isSelected == false) {
					gameObject.GetComponent<Image> ().color = hoverColor;
				}
				inTheCollider = true;
			}
		} else {
			// mouse out of the collider
			if (inTheCollider == true) {
				if (isSelected == false) {
					gameObject.GetComponent<Image> ().color = basicColor;
				}
				inTheCollider = false;
			}
		}
		if(Input.GetMouseButtonDown(0) && inTheCollider == true && isSelected == false) {
			levelSelectionScript.ChangeSelectedCastles (idSelectionCastle);
			gameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("WorldMaps/levelSelectorReady") as Sprite;
			isSelected = true;
		}
	}

	public void unselectIt() {
		gameObject.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("WorldMaps/levelSelector") as Sprite;
		gameObject.GetComponent<Image> ().color = basicColor;
		isSelected = false;
	}


}