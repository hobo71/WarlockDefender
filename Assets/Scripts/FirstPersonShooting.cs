using UnityEngine;
using System.Collections;

public class FirstPersonShooting : MonoBehaviour {

    public Camera playerCamera;
    public GameObject orbPrefab;

    public GameObject[] spellPrefab;
    public GameObject spellAreaPrefab;
    public float spellRange = 10f;

    public GameObject Map;

    public float orbSpeed = 20f;

    private GameObject spellArea;
    private Vector3 spellAreaPosition;
    private int spellIndex = 0;

    void Start () {
        spellArea = null;
    }
	
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject currentOrb = (GameObject)Instantiate(orbPrefab, playerCamera.transform.position + playerCamera.transform.forward, playerCamera.transform.rotation);
            currentOrb.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * orbSpeed, ForceMode.Impulse);

            Physics.IgnoreCollision(currentOrb.GetComponent<Collider>(), GetComponent<Collider>());
            if (spellArea != null)
            {
                Destroy(spellArea);
                spellArea = null;
            }
        }

        if (Input.GetButtonDown("Fire2") && spellArea != null)
        {
            Transform BaseSpellTransform = spellPrefab[spellIndex].GetComponent<Transform>();
            Vector3 positionSpell = new Vector3(spellArea.transform.position.x, BaseSpellTransform.position.y, spellArea.transform.position.z);
            Instantiate(spellPrefab[spellIndex], positionSpell, BaseSpellTransform.transform.rotation);


            if (spellArea != null)
            {
                Destroy(spellArea);
                spellArea = null;
            }
        }

        if (Input.GetKeyDown("1"))
        {
            if (spellArea != null)
            {
                Destroy(spellArea);
                spellArea = null;
            }
            spellArea = (GameObject)Instantiate(spellAreaPrefab, gameObject.transform.position + (gameObject.transform.forward * 10), gameObject.transform.rotation);
            spellIndex = 0;
        }
        else if (Input.GetKeyDown("2"))
        {
            if (spellArea != null)
            {
                Destroy(spellArea);
                spellArea = null;
            }
            spellArea = (GameObject)Instantiate(spellAreaPrefab, gameObject.transform.position + (gameObject.transform.forward * 10), gameObject.transform.rotation);
            spellIndex = 1;
        }

        if (spellArea != null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hitInfo;
            if (Map.GetComponent<Collider>().Raycast(ray, out hitInfo, spellRange))
            {
                spellAreaPosition.x = hitInfo.point.x;
                spellAreaPosition.y = 2f;
                spellAreaPosition.z = hitInfo.point.z;
                spellArea.transform.position = spellAreaPosition;
            }
            else
            {
                spellAreaPosition = playerCamera.transform.position + playerCamera.transform.forward * spellRange;
                spellArea.transform.position = new Vector3(spellAreaPosition.x, 2f, spellAreaPosition.z);
            }
        }


    }
}
