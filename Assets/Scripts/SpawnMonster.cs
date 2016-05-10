using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {
    public GameObject[] monsters;
    public GameObject boss;
	public AddLifeBarEnemy AddLifeBarToConvas;
    public int numberOfEnemies;
    public float spawnTime = 1.0f;
    public int nbrBoss = 0;

    private int index;
	// Use this for initialization
	void Start () {
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawn(Transform location, Transform[] points, Object player)
    {
        index = Random.Range(0, monsters.Length);
        GameObject mob = Instantiate(monsters[index], location.position, location.rotation) as GameObject;
        mob.GetComponent<MoveTo>().points = points;
		AddLifeBarToConvas.addLifeBarToEnemy (mob, mob.GetComponent<EnemieStats>().life);
        mob.GetComponent<MoveTo>().player = player;
    }

    public void spawBoss(Transform location, Transform[] points, Object player)
    {
        GameObject mob = Instantiate(boss, location.position, location.rotation) as GameObject;
        mob.GetComponent<MoveTo>().points = points;
        AddLifeBarToConvas.addLifeBarToEnemy(mob, mob.GetComponent<EnemieStats>().life);
        mob.GetComponent<MoveTo>().player = player;
    }
}
