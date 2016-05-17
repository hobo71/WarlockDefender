using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    Animation animClip;
    
    public Object player;
    public Transform[] points;
    private int index = 0;
    internal NavMeshAgent agent;
    private float speed = 3;
    private float damageToCastle = 10f;
    public bool start = false;
    public bool Maj = true;
    private bool firstTime = true;
    private AEnemyAttack attack;
    internal bool targetPlayer;
    internal bool isStop;
    internal bool isAnim;
    internal bool isWalking;
    internal bool isCastle;
    internal float defaultSpeed;
    public bool isDistante = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        speed = GetComponent<EnemieStats>().animSpeed;
        defaultSpeed = agent.speed;
        damageToCastle = GetComponent<EnemieStats>().damageToCastle;
        attack = GetComponent<AEnemyAttack>();
        if (anim != null)
            anim.enabled = false;
        if (animClip != null)
            animClip.enabled = false;
        targetPlayer = false;
        isStop = false;
        isAnim = false;
        isWalking = false;
        isCastle = false;
    }

    public void ToNextPoint()
    {
        if (points.Length == 0)
            return;
        if (index == points.Length)
        {
            isCastle = true;
            agent.autoBraking = true;
            return;
        }
        Vector3 pos = new Vector3(points[index].position.x + Random.Range(-5, 5), points[index].position.y + Random.Range(-5, 5), points[index].position.z);
        agent.destination = pos;
        ++index;
    }

    void Update()
    {
        if (firstTime)
        {
            if (anim != null)
                anim.enabled = true;
            if (animClip != null)
                animClip.enabled = true;
            ToNextPoint();
            firstTime = false;
        }
        else if (agent.remainingDistance < 0.5f && isCastle)
        {
            CastleStats.life -= damageToCastle;
            Debug.Log("Damage Castle");
            Destroy(gameObject);
        }
        else if (agent.remainingDistance < 0.5f && !targetPlayer)
            ToNextPoint();
        else if (targetPlayer)
        {
            agent.destination = ((GameObject)player).transform.position;
            if (agent.remainingDistance <= 20.0f && isDistante)
            {
                agent.Stop();
                if (Maj == false && !isAnim)
                {
                    animClip["attack"].speed = 2;
                    animClip.Play("attack");
                    isAnim = true;
                    agent.transform.LookAt(((GameObject)player).transform.position);
                    attack.attack(animClip["attack"].length / 2.2f, (GameObject)player, this);
                }
                else if (Maj && !isAnim)
                {
                    animClip["Attack"].speed = 2;
                    animClip.Play("Attack");
                    isAnim = true;
                    agent.transform.LookAt(((GameObject)player).transform.position);
                    attack.attack(animClip["Attack"].length / animClip["Attack"].speed, (GameObject)player, this);
                }
            }
            else if (agent.remainingDistance < 2.0f)
            {
                agent.Stop();
                if (Maj == false && !isAnim)
                {
                    animClip["attack"].speed = 2;
                    animClip.Play("attack");
                    isAnim = true;
                    agent.transform.LookAt(((GameObject)player).transform.position);
                    attack.attack(animClip["attack"].length / animClip["attack"].speed, (GameObject)player, this);
                }
                else if (Maj && !isAnim)
                {
                    animClip["Attack"].speed = 2;
                    animClip.Play("Attack");
                    isAnim = true;
                    agent.transform.LookAt(((GameObject)player).transform.position);
                    attack.attack(animClip["Attack"].length / animClip["Attack"].speed, (GameObject)player, this);
                }
            }
            else
            {
                if (Maj == false && !isWalking)
                {
                    animClip["walk"].speed = speed * 2;
                    animClip.Play("walk");
                    isWalking = true;
                }
                else if (Maj && !isWalking)
                {
                    animClip["Walk"].speed = speed * 2;
                    animClip.Play("Walk");
                    isWalking = true;
                }
                agent.Resume();
                isStop = false;
            }

        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            agent.destination = ((GameObject)player).transform.position;
            targetPlayer = true;
            isAnim = false;
            isStop = false;
            agent.speed = defaultSpeed * 2;
        }
    }

    void OnTriggerExit(Collider other)
    {
        int i = 0;
        float min = 1000000000.0f;
        float distance;

        if (other.gameObject.tag == "Player")
        {
            foreach (Transform point in points)
            {
                distance = Vector3.Distance(point.position, agent.transform.position);
                if (min > distance)
                {
                    index = i;
                    min = distance;
                }
                ++i;
            }
            ++index;
            agent.destination = new Vector3(points[index].position.x + Random.Range(-5, 5), points[index].position.y, points[index].position.z + Random.Range(-5, 5));
            agent.speed = defaultSpeed;
            targetPlayer = false;
            isAnim = false;
            isWalking = false;
            if (Maj)
                animClip.Play("Walk");
            else
                animClip.Play("walk");
            agent.Resume();
        }
    }

    public void isAnimating(bool anim)
    {
        isAnim = anim;
    }
}