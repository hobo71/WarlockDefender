using UnityEngine;
using System.Collections;

public class ThunderSpellBehavior : MonoBehaviour {

	public float lifespan = 3f;
	public float damage = 10f;
	public float timeStun = 1f;

	void Start () {

	}

	void Update () {
		lifespan -= Time.deltaTime;

		if (lifespan <= 0)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			other.GetComponent<PlayerStats>().ApplyDamage(damage);
		if (other.tag == "Enemy") {
			other.GetComponent<EnemieStats> ().ApplyDamage ((int)damage);
			other.GetComponent<EnemieStats> ().ApplyStun (timeStun);
		}
	}
}
