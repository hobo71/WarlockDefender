using UnityEngine;
using System.Collections;

public class PotionBehaviour : MonoBehaviour {

    public float lifeHeal = 100f;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.GetComponentInParent<PlayerStats>();
            playerStats.Heal(lifeHeal);
            Destroy(gameObject);
        }
    }
}
