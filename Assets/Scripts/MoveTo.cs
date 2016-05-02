using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    Animation animClip;
    public Transform[] points;
    private int index = 0;
    private NavMeshAgent agent;
    private int approxX, approxY;
    private float speed = 3;
    public bool start = false;
    public bool Maj = true;
    private bool firstTime = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        speed = GetComponent<EnemieStats>().animSpeed;
        if (anim != null)
            anim.enabled = false;
        if (animClip != null)
            animClip.enabled = false;
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
        if (start && firstTime)
        {
            if (anim != null)
                anim.enabled = true;
            if (animClip != null)
                animClip.enabled = true;
            ToNextPoint();
            firstTime = false;
        }
        else if (agent.remainingDistance < 0.5f && start)
            ToNextPoint();
        if (anim != null)
            anim.speed = speed;
        if (animClip != null)
        {
            if (Maj == false)
                animClip["walk"].speed = speed;
            if (Maj == true)
                animClip["Walk"].speed = speed;
        }
    }
}