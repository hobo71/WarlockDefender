using UnityEngine;
using System.Collections;

public class MagicBallBehaviour : MonoBehaviour {

    public float lifespan = 3f;
    public float damage = 10f;
    public float speed = 20f;

    void Start () {
        //Camera playerCamera = GameObject.Find("POV Camera").GetComponent<Camera>();
        //GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * speed, ForceMode.Impulse);
    }
	
	void Update () {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
            col.gameObject.GetComponent<EnemieStats>().ApplyDamage((int)damage);
        Destroy(gameObject);
    }
}
