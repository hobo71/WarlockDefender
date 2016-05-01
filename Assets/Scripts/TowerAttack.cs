using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour {
    [SerializeField]
    Rigidbody arrow;
    [SerializeField]
    Transform weapon;

    public int damage = 10;
    public float attackSpeed = 0.5f;
    public Transform spawn;
    private List<GameObject> _enemies;
    private EnemieStats enemie;
    internal bool _update;
    internal float time;
    private Rigidbody arrowClone;
    private float orientation;
    private float orientationTarget;
    private Quaternion lookAt;
    private Vector3 angle;
    private Quaternion startAngle;
    public bool start = false;

    // Use this for initialization
    void Start () {
        _enemies = new List<GameObject>();
        _update = true;
        time = 0.0f;
        arrowClone = null;
        spawn.position = new Vector3(spawn.position.x + 1, spawn.position.y, spawn.position.z);
        angle = new Vector3(0,0,0);
        startAngle = weapon.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (start == false)
            return;
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
                if (_enemies[0] == null)
                    return;
                enemie = _enemies[0].GetComponent<EnemieStats>();
                weapon.LookAt(enemie.transform);
                weapon.rotation = new Quaternion(0, spawn.rotation.y, 0, spawn.rotation.w);
                _update = false;
            }
        }
        if (_enemies.Count > 0)
        {
            weapon.LookAt(enemie.transform);
            weapon.rotation = new Quaternion(0, weapon.rotation.y, 0, weapon.rotation.w);
        }
        if (time >= attackSpeed)
        {
            arrowClone = (Rigidbody)Instantiate(arrow, spawn.position, weapon.rotation);
            if (arrowClone == null || enemie == null)
                return;
            arrowClone.transform.LookAt(enemie.transform);
            enemie.ApplyDamage(damage);
            time = 0.0f;
        }
        if (arrowClone != null)
        {
            if (_enemies.Count > 0)
                angle = _enemies[0].transform.position - arrowClone.transform.position;
            arrowClone.AddForce(angle * 20);
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
        weapon.rotation = startAngle;
    }

    public void StartAtt()
    {
        start = true;
    }
}
