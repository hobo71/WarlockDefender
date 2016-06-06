using UnityEngine;
using System.Collections;

public class BlipScript : MonoBehaviour {

    public Transform target;

    private MiniMapScript map;
    private RectTransform myRectTransform;

	void Start () {
        map = GetComponentInParent<MiniMapScript>();
        myRectTransform = GetComponent<RectTransform>();
	}
	
	void LateUpdate () {
        if (!target) {
            Destroy(gameObject);
            return;
        }
        Vector2 newPosition = map.TransformPosition(target.position);
        myRectTransform.anchoredPosition = newPosition;
    }
}
