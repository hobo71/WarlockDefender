using UnityEngine;
using System.Collections;

public class ParticleTrailRotation : MonoBehaviour {

    public Vector3 rotationSpeed;
    public bool local;

    void Start () {
	
	}
	
	void Update () {
        if (local)
            transform.Rotate(rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(rotationSpeed * Time.deltaTime, Space.World);
    }
}
