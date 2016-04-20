using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour {
    public int damage = 10;
    public float attackSpeed = 0.5f;
    private List<GameObject> _enemies;
    private EnemieStats enemie;
    internal bool _update;
    internal float time;

	// Use this for initialization
	void Start () {
        _enemies = new List<GameObject>();
        _update = true;
        time = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemies.Count == 0)
            return;
        time += Time.deltaTime;
        if (enemie == null && !_update)
        {
            _update = true;
            _enemies.RemoveAt(0);
        }
        if (_update)
        {
            if (_enemies.Count > 0)
            {
                enemie = _enemies[0].GetComponent<EnemieStats>();
                _update = false;
            }
        }
        if (time >= attackSpeed)
        {
            enemie.ApplyDamage(damage);
            time = 0.0f;
        }
 	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemie")
        {
            _enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        _enemies.Remove(other.gameObject);
    }
}
