using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public float life = 100f;

    void Start () {
	
	}
	
	void Update () {
        if (life <= 0)
        {
            //Destroy(transform.parent.gameObject);
        }
    }

    public void ApplyDamage(float damage)
    {
        life -= damage;
    }

    public void Heal(float potionLife)
    {
        life += potionLife;
    }
}
