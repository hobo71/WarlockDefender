using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpawnTimerCollider : MonoBehaviour {

    //public Collider collider;

    public float timer = 1f;

    private float incrementTime = 0f;

	void Start () {

	}
	
	void Update () {
        incrementTime += Time.deltaTime;

        if (incrementTime >= timer)
            GetComponent<Collider>().enabled = true;
	}
}
