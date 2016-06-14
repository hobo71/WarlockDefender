using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {
    public GameObject[] monsters;
    public GameObject boss;
	public AddLifeBarEnemy AddLifeBarToConvas;
    public int numberOfEnemies;
    public float spawnTime = 1.0f;
    public int nbrBoss = 0;
    public float lifeMultiplicator = 1.0f;

    public GameObject EnemyBlipPrefab;
    public GameObject cameraTexture;

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
        if (lifeMultiplicator != 1.0f)
            mob.GetComponent<EnemieStats>().life *= lifeMultiplicator;
		AddLifeBarToConvas.addLifeBarToEnemy (mob, mob.GetComponent<EnemieStats>().life);
        mob.GetComponent<MoveTo>().player = player;

        GameObject enemyBlip = Instantiate(EnemyBlipPrefab, location.position, location.rotation) as GameObject;
        enemyBlip.transform.SetParent(cameraTexture.transform, false);
        BlipScript scriptBlip = enemyBlip.GetComponent<BlipScript>();
        scriptBlip.target = mob.transform;
    }

    public void spawBoss(Transform location, Transform[] points, Object player)
    {
        GameObject mob = Instantiate(boss, location.position, location.rotation) as GameObject;
        mob.GetComponent<MoveTo>().points = points;
        AddLifeBarToConvas.addLifeBarToEnemy(mob, mob.GetComponent<EnemieStats>().life);
        mob.GetComponent<MoveTo>().player = player;

        GameObject enemyBlip = Instantiate(EnemyBlipPrefab, location.position, location.rotation) as GameObject;
        enemyBlip.transform.SetParent(cameraTexture.transform, false);
        BlipScript scriptBlip = enemyBlip.GetComponent<BlipScript>();
        scriptBlip.target = mob.transform;
    }
}
