using UnityEngine;
using System.Collections;

public class MoveSpellArea : MonoBehaviour {

    public float spellRange = 10f;

    private GameObject map;
    private Camera playerCamera;
    private Vector3 spellAreaPosition;
    void Start () {
        map = GameObject.Find("Terrain");
        playerCamera = GameObject.Find("POV Camera").GetComponent<Camera>();
    }
	
	void Update () {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hitInfo;
        if (map.GetComponent<Collider>().Raycast(ray, out hitInfo, spellRange))
        {
            spellAreaPosition.x = hitInfo.point.x;
            spellAreaPosition.y = 0.01f;
            spellAreaPosition.z = hitInfo.point.z;
            this.transform.position = spellAreaPosition;
        }
        else
        {
            spellAreaPosition = playerCamera.transform.position + playerCamera.transform.forward * spellRange;
            this.transform.position = new Vector3(spellAreaPosition.x, 0.01f, spellAreaPosition.z);
        }
    }
}
