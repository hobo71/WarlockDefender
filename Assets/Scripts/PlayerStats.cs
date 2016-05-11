using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public float maxlife = 100f;
    private float currentlife = 100f;

    void Start () {
        currentlife = maxlife;
    }
	
	void Update () {
        if (currentlife <= 0)
        {
            //Destroy(transform.parent.gameObject);
        }
    }

    public void ApplyDamage(float damage)
    {
        currentlife -= damage;
    }

    public void Heal(float potionLife)
    {
        if (currentlife + potionLife > maxlife)
            currentlife = maxlife;
        else
            currentlife += potionLife;
    }

    public void ResetLife()
    {
        currentlife = maxlife;
    }

    public float GetCurrentLife()
    {
        return currentlife;
    }
}
