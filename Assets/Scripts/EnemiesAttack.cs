﻿using UnityEngine;
using System.Collections;

public class EnemiesAttack : MonoBehaviour, AEnemyAttack
{

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
        if (move.agent.remainingDistance < 3.5f)
            player.GetComponent<PlayerStats>().ApplyDamage(damage);
        move.isAnimating(false);
        move.isWalking = false;
    }

    public void attack(float duration, GameObject player, MoveTo move)
    {
        StartCoroutine(attackPlayer(duration, player, move));
    }
}
