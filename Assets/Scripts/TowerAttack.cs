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
                float angleX = Random.Range(30 / 2, 30);
                float angleY = Random.Range(20 / 2, 30);
                if (Random.Range(0f, 1f) > 0.5f) angleY *= -1;
                enemie = _enemies[0].GetComponent<EnemieStats>();
                weapon.LookAt(enemie.transform);
                weapon.rotation = new Quaternion(0, spawn.rotation.y, 0, spawn.rotation.w);
                _update = false;
            }
        }
        if (_enemies.Count > 0)
        {
            //float angleX = Random.Range(30 / 2, 30);
            //float angleY = Random.Range(20 / 2, 30);
            //if (Random.Range(0f, 1f) > 0.5f) angleY *= -1;
            //weapon.LookAt(enemie.transform.position);
            //weapon.rotation = new Quaternion(0, spawn.rotation.y, 0, spawn.rotation.w);
            //orientationTarget = _enemies[0].transform.position.z;
            //orientation = weapon.transform.position.z;
            //if (orientation > orientationTarget)
            //    lookAt = Quaternion.LookRotation(_enemies[0].transform.position - weapon.position, Vector3.left);
            //else
            //    lookAt = Quaternion.LookRotation(_enemies[0].transform.position - weapon.position, Vector3.right);
            //lookAt.x = 0.0f;
            //lookAt.z = 0.0f;
            //weapon.rotation = Quaternion.Lerp(weapon.rotation, lookAt, Time.fixedDeltaTime * 2.0f);

            weapon.LookAt(enemie.transform);
            weapon.rotation = new Quaternion(0, weapon.rotation.y, 0, weapon.rotation.w);
            //weapon.transform.RotateAround(transform.position, transform.up, 90);
            //weapon.transform.RotateAround(transform.position, transform.up, 90);

            //weapon.rotation = new Quaternion(0, weapon.rotation.y, 0, weapon.rotation.w);
            //orientation = fire.transform.position.z;
            //if (orientation > orientationTarget)
            //    lookAt = Quaternion.LookRotation(_enemies[0].transform.position - spawn.position, Vector3.left);
            //else
            //    lookAt = Quaternion.LookRotation(_enemies[0].transform.position - spawn.position, Vector3.right);
            //lookAt.x = 0.0f;
            //lookAt.z = 0.0f;
            //fire.transform.rotation = Quaternion.Lerp(fire.transform.rotation, lookAt, Time.smoothDeltaTime * 1.0f);
        }
        if (time >= attackSpeed)
        {
            arrowClone = (Rigidbody)Instantiate(arrow, spawn.position, weapon.rotation);
            arrowClone.transform.LookAt(enemie.transform);

            //            arrowClone.transform.Rotate(new Vector3(0, weapon.rotation.y, 110));
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
}
