using UnityEngine;
using System.Collections;

public class DistanteAttack : MonoBehaviour, AEnemyAttack {
    public int damage = 20;
    public GameObject projectile;
    //private Rigidbody rdby = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator attackPlayer(float duration, GameObject player, MoveTo move)
    {
        yield return new WaitForSeconds(duration);
        Vector3 pos = move.agent.transform.position;
        pos.y += 1;
        GameObject pro = Instantiate(projectile, pos, move.agent.transform.rotation) as GameObject;
        Rigidbody rdby = pro.GetComponent<Rigidbody>();//.AddForce(move.agent.transform.forward * 200, ForceMode.Impulse);
        rdby.AddForce(transform.forward * 20, ForceMode.Impulse);
        move.isAnimating(false);
        move.isWalking = false;
    }

    public void attack(float duration, GameObject player, MoveTo move)
    {
        StartCoroutine(attackPlayer(duration, player, move));
    }
}
