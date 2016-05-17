using UnityEngine;
using System.Collections;

public interface AEnemyAttack {

    void attack(float duration, GameObject player, MoveTo move);
}
