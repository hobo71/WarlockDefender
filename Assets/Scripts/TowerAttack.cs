using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour {
    [SerializeField]
    Rigidbody arrow;

    public int damage = 10;
    public float attackSpeed = 0.5f;
    public Transform towerPos;
    private List<GameObject> _enemies;
    private EnemieStats enemie;
    internal bool _update;
    internal float time;
    private Rigidbody arrowClone;

    // Use this for initialization
    void Start () {
        _enemies = new List<GameObject>();
        _update = true;
        time = 0.0f;
        arrowClone = null;
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
            arrowClone = null;
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
            arrowClone = (Rigidbody)Instantiate(arrow, new Vector3(towerPos.position.x, towerPos.position.y + 10, towerPos.position.z), arrow.transform.rotation);
            enemie.ApplyDamage(damage);
            time = 0.0f;
        }
        if (arrowClone != null)
        {

            var angle = _enemies[0].transform.position - arrowClone.transform.position;
            arrowClone.AddForce(angle * 30);
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
