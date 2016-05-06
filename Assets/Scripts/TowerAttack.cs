using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour {
    [SerializeField]
    GameObject arrowPrefab;
    [SerializeField]
    Transform weapon;

    public int damage = 10;
    public float fireCooldown = 0.5f;
    public Transform spawn;
    private List<GameObject> _enemies;
    private EnemieStats enemy;
    internal bool _update;
    internal float time;
    private float orientation;
    private float orientationTarget;
    private Quaternion lookAt;
    private Quaternion startAngle;
    public bool start = false;

    // Use this for initialization
    void Start () {
        _enemies = new List<GameObject>();
        _update = true;
        time = 0.0f;
        spawn.position = new Vector3(spawn.position.x + 1, spawn.position.y, spawn.position.z);
        startAngle = weapon.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (start == false)
            return;
        if (_enemies.Count == 0)
            return;
        time += Time.deltaTime;
        if (enemy == null && !_update)
        {
            _update = true;
            _enemies.RemoveAt(0);
        }
        if (_update)
        {
            if (_enemies.Count > 0)
            {
                if (_enemies[0] == null)
                    return;
                enemy = _enemies[0].GetComponent<EnemieStats>();
                weapon.LookAt(enemy.transform);
                weapon.rotation = new Quaternion(0, spawn.rotation.y, 0, spawn.rotation.w);
                _update = false;
            }
        }
        if (_enemies.Count > 0)
        {
            weapon.LookAt(enemy.transform);
            weapon.rotation = new Quaternion(0, weapon.rotation.y, 0, weapon.rotation.w);
        }
        if (time >= fireCooldown)
        {
            if (enemy == null)
                return;
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, spawn.position, weapon.rotation);
            ArrowBehaviour a = arrow.GetComponent<ArrowBehaviour>();
            a.target = enemy.gameObject.transform;
            time = 0.0f;
        }
 	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy detected");
            _enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Enemy out of range");
        _enemies.Remove(other.gameObject);
        weapon.rotation = startAngle;
    }

    public void StartAtt()
    {
        start = true;
    }
}
