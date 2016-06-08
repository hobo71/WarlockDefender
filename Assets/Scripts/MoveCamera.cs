using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public float panSpeed = 30.0f;

    public bool tutoMode;
    
    private bool goToCastle = false;
    private bool goToStartPoint = false;
    public GameObject TutoSpawn = null;
    public GameObject TutoCastle = null;
    public GameObject TutoTowers = null;
    public GameObject blackBackground = null;
    
    public GameObject infoPanel;
    public GameObject castleInfosPanel;
    public GameObject towerPlacementPanel;

    public int delta = 10;

    private Vector3 mouseOrigin;

    private Vector3 spawnPoint = new Vector3(143.9f, 27f, 45.9f);
    private Vector3 startPoint = new Vector3(89f, 27f, 77f);
    private Vector3 castlePoint = new Vector3(-6.1f, 36.1f, 138.5f);
    public float speedGoToCastle;
    public float speedGoToStartPoint;
    private float startTime;
    private float journeyLength;
    // Use this for initialization
    void Start () {
        GameObject obj = GameObject.Find ("MenuDatasContainer");
		if (obj) {
			tutoMode = obj.GetComponent<DataContainerScript> ().TutorialActivation;
        }
        if (tutoMode) {
            transform.position = spawnPoint;
            TutoSpawn.SetActive(true);
            TutoCastle.SetActive(false);
            TutoTowers.SetActive(false);
            blackBackground.SetActive(true);
            infoPanel.SetActive(false);
            castleInfosPanel.SetActive(false);
            towerPlacementPanel.SetActive(false);
        } else {
            transform.position = startPoint;
            if (TutoSpawn && TutoCastle && TutoTowers && blackBackground) {
                TutoSpawn.SetActive(false);
                TutoCastle.SetActive(false);
                TutoTowers.SetActive(false);
                blackBackground.SetActive(false);                
            }
            infoPanel.SetActive(true);
            castleInfosPanel.SetActive(true);
            towerPlacementPanel.SetActive(true);
        }
        
	}


    public void ShowAllPlacementPanels() {
        infoPanel.SetActive(true);
        castleInfosPanel.SetActive(true);
        towerPlacementPanel.SetActive(true);
    }

    public void SetTutoMode(bool tuto) {
        tutoMode = tuto;
    }

    public void StartGoToCastle() {
        goToCastle = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(spawnPoint, castlePoint);
    }
    
     public void GoToStartPoint() {
        goToStartPoint = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(castlePoint, startPoint);
    }

    // Update is called once per frame
    void Update() {
        if (tutoMode) {
            if (goToCastle) {
                float distCovered = (Time.time - startTime) * speedGoToCastle;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(spawnPoint, castlePoint, fracJourney);
                
                if (transform.position == castlePoint) {
                    goToCastle = false;
                    TutoCastle.SetActive(true);
                    blackBackground.SetActive(true);
                }
            }
            
             if (goToStartPoint) {
                float distCovered = (Time.time - startTime) * speedGoToStartPoint;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(castlePoint, startPoint, fracJourney);
                
                if (transform.position == startPoint) {
                    goToStartPoint = false;
                    TutoTowers.SetActive(true);
                    blackBackground.SetActive(true);
                    ShowAllPlacementPanels();
                }
            }
            
            return;
        }
        Cursor.visible = true;
        mouseOrigin = Input.mousePosition;
        //Vector3 pos = gameObject.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition);
        //Vector3 move = pos;
        if (mouseOrigin.x <= 0 + delta && transform.position.z <= 153f) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (mouseOrigin.x >= (Screen.width - delta) && transform.position.z >= 48f) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (mouseOrigin.y <= 0 + delta && transform.position.x >= 5f) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (mouseOrigin.y >= (Screen.height - delta) && transform.position.x <= 137f) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
    }
}
