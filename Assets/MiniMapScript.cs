using UnityEngine;
using System.Collections;

public class MiniMapScript : MonoBehaviour {

    public Camera cam;
    public RectTransform mapRect;

    void Start () {
    }
	
	void Update () {

	}

    public Vector2 TransformPosition(Vector3 position)
    {
        Vector2 ScreenPos = cam.WorldToViewportPoint(position);
        //Debug.Log(mapRect.sizeDelta);
        ScreenPos.x *= mapRect.sizeDelta.x;
        ScreenPos.y *= mapRect.sizeDelta.y;

        return ScreenPos;
    }
}
