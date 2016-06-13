using UnityEngine;
using System.Collections;

public class FirstPersonShooting : MonoBehaviour {

    public Camera playerCamera;
    //public GameObject orbPrefab;
	public FPSPanelScript fpsPanel;

    public GameObject[] spellPrefab;
    public GameObject[] spellAreaPrefab;
    public float spellRange = 10f;

    public GameObject Map;

    //public float orbSpeed = 20f;

    private GameObject currentSpellArea;
    private Vector3 spellAreaPosition;
    private int spellIndex = 0;
    private bool spellReady = false;

    void Start () {
        currentSpellArea = null;
    }
	
	void Update () {
   //     if (Input.GetButtonDown("Fire1"))
   //     {
   //         GameObject currentOrb = (GameObject)Instantiate(orbPrefab, playerCamera.transform.position + playerCamera.transform.forward, playerCamera.transform.rotation);
   //         currentOrb.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * orbSpeed, ForceMode.Impulse);

   //         Physics.IgnoreCollision(currentOrb.GetComponent<Collider>(), GetComponent<Collider>());
   //         if (currentSpellArea != null)
   //         {
   //             Destroy(currentSpellArea);
   //             currentSpellArea = null;
   //         }
			//fpsPanel.TurnAllSpellsToFalse ();
   //     }

        if (Input.GetButtonDown("Fire1") && spellReady) {
			Transform BaseSpellTransform = spellPrefab [spellIndex].GetComponent<Transform> ();
			Vector3 positionSpell;

			if (currentSpellArea) {
				positionSpell = new Vector3 (currentSpellArea.transform.position.x, BaseSpellTransform.position.y, currentSpellArea.transform.position.z);
			} else {
				positionSpell = playerCamera.transform.position + playerCamera.transform.forward;
			}
			GameObject spell = (GameObject)Instantiate(spellPrefab[spellIndex], positionSpell, BaseSpellTransform.transform.rotation);
			Physics.IgnoreCollision(spell.GetComponent<Collider>(), GetComponent<Collider>());


            if (currentSpellArea != null) {
                Destroy(currentSpellArea);
                currentSpellArea = null;
            }
            spellReady = false;
			fpsPanel.StartCoolDownCurrentSpell ();
			fpsPanel.TurnAllSpellsToFalse ();
            fpsPanel.removeTuto();
        }
    }

	public void newSpellIsSelected(int newSpellId) {
		if (currentSpellArea != null) {
			Destroy(currentSpellArea);
            currentSpellArea = null;
		}
        if (spellAreaPrefab[newSpellId])
        	currentSpellArea = (GameObject)Instantiate(spellAreaPrefab[newSpellId], gameObject.transform.position + (gameObject.transform.forward * 10), gameObject.transform.rotation);
		
		spellIndex = newSpellId;
        spellReady = true;
	}

}
