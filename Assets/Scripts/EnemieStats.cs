using UnityEngine;
using System.Collections;

public class EnemieStats : MonoBehaviour {

    public int life = 100;
    public float animSpeed = 2;
    [SerializeField]
    GameObject enemie;

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
            Destroy(enemie);
        }                    
	}
}
