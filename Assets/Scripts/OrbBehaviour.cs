using UnityEngine;
using System.Collections;

public class OrbBehaviour : MonoBehaviour {

    public float lifespan = 3f;

	void Start () {
	
	}
	
	void Update () {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }
}
