using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapDamage : MonoBehaviour {
    [SerializeField]
    Animation anim;
    public int damage = 10;
    public float AttackSpeed = 1.0f;

    private List<GameObject> _enemies;
    internal float _time;
    private bool _anim;

	// Use this for initialization
	void Start () {
        anim.Stop("Attack");
        _enemies = new List<GameObject>();
        _anim = false;
    }
	
	// Update is called once per frame
	void Update () {
        _time += Time.deltaTime;
        if (_enemies.Count > 0 && _time >= AttackSpeed)
        {
            StartCoroutine(ApplyDamage());
        }
	}

    IEnumerator ApplyDamage()
    {
        yield return new WaitForSeconds(0.05f);
        _anim = false;
        foreach (GameObject enemy in _enemies)
        {
            if (enemy == null)
                continue;
            enemy.GetComponent<EnemieStats>().ApplyDamage(damage);
            _anim = true;
        }
        if (_anim)
            anim.Play("Attack");
        _time = 0.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!_enemies.Exists(x => x == other.gameObject))
                _enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _enemies.Remove(other.gameObject);
        }
    }

}
