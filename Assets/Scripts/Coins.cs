using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour {
    [SerializeField]
    GameObject coin;
    private LevelManager manager;
    // Use this for initialization
    void Start () {
        manager = GameObject.FindGameObjectWithTag("Scripts").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            manager.money += 50;
            Destroy(coin);
        }
    }
}
