using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickColliderLevel : MonoBehaviour
{
	public LevelSelection levelSelectionScript;
	public int idSelectionCastle;

	private Color basicColor = new Color(1, 1, 1);
	private Color selectedColor = new Color(0, 0, 1);
	private Color hoverColor = new Color(0.5f, 0.5f, 0.5f);

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
			gameObject.GetComponent<Image> ().color = selectedColor;
			isSelected = true;
		}
	}

	public void unselectIt() {
		gameObject.GetComponent<Image> ().color = basicColor;
		isSelected = false;
	}


}