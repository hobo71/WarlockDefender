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
    internal float time;
    private Quaternion startAngle;

    // Use this for initialization
    void Start () {
        _enemies = new List<GameObject>();
        time = 0.0f;
        spawn.position = new Vector3(spawn.position.x + 1, spawn.position.y, spawn.position.z);
        startAngle = weapon.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemies.Count <= 0)
            return;
        time += Time.deltaTime;
        if (_enemies.Count > 0 && _enemies[0] == null)
            _enemies.RemoveAt(0);
        if (_enemies[0] != null)
        {
            weapon.LookAt(_enemies[0].transform);
            weapon.rotation = new Quaternion(0, weapon.rotation.y, 0, weapon.rotation.w);
        }
        if (time >= fireCooldown)
        {
            GameObject arrow = (GameObject)Instantiate(arrowPrefab, spawn.position, weapon.rotation);
            ArrowBehaviour a = arrow.GetComponent<ArrowBehaviour>();
            a.target = _enemies[0].gameObject.transform;
            time = 0.0f;
        }
 	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            weapon.rotation = startAngle;
            _enemies.Remove(other.gameObject);
        }
    }
}
