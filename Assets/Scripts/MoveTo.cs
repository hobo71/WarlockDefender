using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    public Transform[] points;
    private int index = 0;
    private NavMeshAgent agent;
    private int approxX, approxY;
    private float speed = 3;
    public bool start = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        speed = GetComponent<EnemieStats>().animSpeed;
        anim.enabled = false;
    }

    public void ToNextPoint()
    {
        if (points.Length == 0)
            return;
        if (index == points.Length)
        {
            agent.autoBraking = true;
            return;
        }
        Vector3 pos = new Vector3(points[index].position.x + Random.Range(-5, 5), points[index].position.y + Random.Range(-5, 5), points[index].position.z);
        agent.destination = pos;
        ++index;
    }

    void Update()
    {
        if (start)
        {
            anim.enabled = true;
            ToNextPoint();
        }
        if (agent.remainingDistance < 0.5f && start)
        {
            anim.enabled = true;
            ToNextPoint();
        }
        if (anim != null)
            anim.speed = speed;
    }
}