﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour {
    //[SerializeField]
    //GameObject arrowPrefab;
    [SerializeField]
    Transform weapon;
    [SerializeField]
    TowerStats towerStats; 

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
        while (_enemies.Count > 0 && _enemies[0] == null)
            _enemies.RemoveAt(0);
        if (_enemies.Count <= 0)
            return;
        time += Time.deltaTime;
        weapon.LookAt(_enemies[0].transform);
        weapon.rotation = new Quaternion(0, weapon.rotation.y, 0, weapon.rotation.w);
        if (time >= towerStats.attackSpeed)
        {
            Debug.Log("Shoot");
            GameObject arrow = (GameObject)Instantiate(towerStats.projectilePrefab, spawn.position, weapon.rotation);
            ArrowBehaviour a = arrow.GetComponent<ArrowBehaviour>();
            a.areaOfEffect = towerStats.spreadZone;
            a.damage = towerStats.projectileDamage;
            a.speed = towerStats.projectileSpeed;
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
