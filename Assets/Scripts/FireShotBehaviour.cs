using UnityEngine;
using System.Collections;

public class FireShotBehaviour : MonoBehaviour {

    public float lifespan = 3f;
    public float damage = 10f;

    void Start () {
	
	}
	
	void Update () {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<PlayerStats>().ApplyDamage(damage);
    }
}