using UnityEngine;
using System.Collections;

public class EnemieStats : MonoBehaviour {

    public int life = 100;
    public float animSpeed = 2;
    public float damageToCastle = 10f;
    [SerializeField]
    GameObject enemie;
    [SerializeField]
    GameObject coin;

    private int random;

    // Use this for initialization
    void Start () {
	}
	
    public void ApplyDamage(int damage)
    {
        life -= damage;
    }

	// Update is called once per frame
	void Update () {
        if (life <= 0 && enemie != null)
        {
            random = Random.Range(0, 5);
            Debug.Log(random);
            if (random == 2)
                Instantiate(coin, new Vector3(enemie.transform.position.x, enemie.transform.position.y + 1, enemie.transform.position.z), enemie.transform.rotation);
            Destroy(enemie);
        }                    
	}
}
