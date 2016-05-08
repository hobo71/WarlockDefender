using UnityEngine;
using System.Collections;

public class IceFloorBehaviour : MonoBehaviour {

    public float lifespan = 5f;

    private float time = 0f;

	void Start () {
        time = lifespan;
    }
	
	void Update () {

        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(gameObject);
            time = lifespan;
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            NavMeshAgent navmeshAgent = other.GetComponentInParent<NavMeshAgent>();

            navmeshAgent.speed -= 1f;
            if (navmeshAgent.speed < 0)
                navmeshAgent.speed = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            NavMeshAgent navmeshAgent = other.GetComponentInParent<NavMeshAgent>();

            navmeshAgent.speed += 1f;
        }
    }
}
