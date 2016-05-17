using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
    public Object Player;
    public int damage;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<PlayerStats>().ApplyDamage(damage);
//        Destroy(this.gameObject);
    }
}
