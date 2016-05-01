using UnityEngine;
using System.Collections;

public class DestroyArrow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
//        if (collision.gameObject.name == "Terrain" || collision.gameObject.tag == "Enemie" || collision.gameObject.tag == "Player")
//        {
        var arrow = GetComponent<Rigidbody>();
        if (arrow != null)
            Destroy(arrow.gameObject);
//        }
    }
}
