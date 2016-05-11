using UnityEngine;
using System.Collections;

public class EnemiesAttack : MonoBehaviour {

    public int damage = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator attackPlayer(float duration, GameObject player, MoveTo move)
    {
        yield return new WaitForSeconds(duration);
        if (move.agent.remainingDistance < 2.5f)
            player.GetComponent<PlayerStats>().ApplyDamage(damage);
        move.isAnimating(false);
        move.isWalking = false;
    }

    public void attack(float duration, GameObject player, MoveTo move)
    {
        StartCoroutine(attackPlayer(duration, player, move));
    }
}
