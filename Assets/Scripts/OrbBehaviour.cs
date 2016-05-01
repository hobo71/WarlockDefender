using UnityEngine;
using System.Collections;

public class OrbBehaviour : MonoBehaviour {

    public float lifespan = 3f;
    public float damage = 5f;

	void Start () {
	
	}
	
	void Update () {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemie")
            col.gameObject.GetComponent<EnemieStats>().ApplyDamage((int)damage);
        Destroy(gameObject);
    }
}
