using UnityEngine;
using System.Collections;

public class MiniMapScript : MonoBehaviour {

    public Camera cam;
    public RectTransform mapRect;

    void Start () {
	    mapRect = GetComponent<RectTransform>();
    }
	
	void Update () {

	}

    public Vector2 TransformPosition(Vector3 position)
    {
        Vector2 ScreenPos = cam.WorldToViewportPoint(position);
        ScreenPos.x *= mapRect.sizeDelta.x;
        ScreenPos.y *= mapRect.sizeDelta.y;

        ScreenPos.x -= mapRect.sizeDelta.x * mapRect.pivot.x;
        ScreenPos.y -= mapRect.sizeDelta.y * mapRect.pivot.y;

        return ScreenPos;
    }
}
