using UnityEngine;
using System.Collections;

public class TryPathIsOk : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    NavMeshPath path = new NavMeshPath();
        GetComponent<NavMeshAgent>().CalculatePath(destination, path);
        if (path.status == NavMeshPathStatus.PathPartial)
	        Console.Log()
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
