using UnityEngine;
using System.Collections;

public class CooldownScript : MonoBehaviour {

    public float cooldown = 4f;
    public bool isInCooldown = false;

    private float timer = 0.0f;
	void Start () {

	}
	
	void Update () {
	    if (isInCooldown)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                isInCooldown = false;
                timer = 0.0f;
            }
        }
	}


}
