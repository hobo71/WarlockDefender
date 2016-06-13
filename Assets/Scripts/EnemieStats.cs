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

	private MoveTo moveToScript;

	private int random;
	private bool isStun;
    private float stunTime;

    // Use this for initialization
    void Start () {
		moveToScript = GetComponent<MoveTo> ();
		isStun = false;
		stunTime = 0f;
	}
	
    public void ApplyDamage(float damage)
    {
        life -= damage;
    }

	public void ApplyStun(float time)
	{
		stunTime = time;
		isStun = true;
		moveToScript.StopMovement(true);
		Debug.Log ("Apply Stun");
	}

	// Update is called once per frame
	void Update () {
		if (isStun && stunTime > 0f) {
			stunTime -= Time.deltaTime;
		} else if (isStun && stunTime <= 0f) {
			isStun = false;
			stunTime = 0f;
			moveToScript.StopMovement(false);
		}

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
