using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {
    public GameObject[] monsters;
	public AddLifeBarEnemy AddLifeBarToConvas;
    public int numberOfEnemies;
    public float spawnTime = 1.0f;

    private int index;
	// Use this for initialization
	void Start () {
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawn(Transform location, Transform[] points)
    {
        index = Random.Range(0, monsters.Length);
        GameObject mob = Instantiate(monsters[index], location.position, location.rotation) as GameObject;
        mob.GetComponent<MoveTo>().points = points;
		AddLifeBarToConvas.addLifeBarToEnemy (mob, mob.GetComponent<EnemieStats>().life);
    }
}
