using UnityEngine;
using System.Collections;

public class EnemieStats : MonoBehaviour {

    public float life = 100f;
    public float animSpeed = 2;
    public float damageToCastle = 10f;
    [SerializeField]
    GameObject enemie;
    [SerializeField]
    GameObject coin;
    [SerializeField]
    GameObject potion;

    private int random;

    // Use this for initialization
    void Start () {
	}
	
    public void ApplyDamage(float damage)
    {
        life -= damage;
    }

	// Update is called once per frame
	void Update () {
        if (life <= 0 && enemie != null)
        {
            random = Random.Range(0, 20);
            if (random == 2)
                Instantiate(potion, new Vector3(enemie.transform.position.x + 2, enemie.transform.position.y + 1, enemie.transform.position.z), enemie.transform.rotation);
            Instantiate(coin, new Vector3(enemie.transform.position.x, enemie.transform.position.y + 1, enemie.transform.position.z), enemie.transform.rotation);

            Destroy(enemie);
        }                    
	}
}
