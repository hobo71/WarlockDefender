using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    GameObject[] particleSpawner;
    [SerializeField]
    Object Player;

    public SpawnMonster[] Waves;
    public GameObject[] Spawner;

    private static bool start;
    private bool isSpawning;
    private static SpawnMonster current;
    private static int index;

    private int nbr;
    private int indexSpawner;

    private static bool activate;
    private static int nbrWave;

    private int nbrBoss = 0;

	// Use this for initialization
	void Start () {
        start = false;
        activate = false;
        isSpawning = false;
        current = null;
        index = 0;
        nbrWave = Waves.Length;
        current = Waves[index];
        nbr = current.numberOfEnemies;
        nbrBoss = current.nbrBoss;
    }

    // Update is called once per frame
    void Update () {
	    if (start)
        {
            if (index < Waves.Length)
            {
                if (current == null)
                {
                    current = Waves[index];
                    activate = true;
                    nbr = current.numberOfEnemies;
                    nbrBoss = current.nbrBoss;
                }
                if (!activate)
                    activate = true;
                if (nbr <= 0)
                {
                    start = false;
                    current = null;
                }
                if (current != null && !isSpawning)
                {
                    isSpawning = true;
                    indexSpawner = Random.Range(0, Spawner.Length);
                    StartCoroutine(SpawnMonster(indexSpawner));
                }
            }
        }
    }

    IEnumerator SpawnMonster(int index)
    {
        yield return new WaitForSeconds(current.spawnTime);
        Transform[] points = Spawner[index].GetComponent<WayPoints>().points;
        if (nbr <= nbrBoss)
            current.spawBoss(Spawner[index].transform, points, Player);
        else
            current.spawn(Spawner[index].transform, points, Player);
        --nbr;
        isSpawning = false;
    }

    public static bool isEnemiesAlive()
    {
		if (GameObject.FindGameObjectWithTag("Enemy") == null && current == null && activate) {
            ++index;
            activate = false;
			return false;
		}
		return true;
    }

    public static bool isLastWave()
    {
        if (index >= nbrWave)
            return true;
        return false;
    }

    public void resetWaves()
    {
        index = 0;
        current = null;
        nbrWave = Waves.Length;
        nbr = 1;
    }

    public void startWaves() { start = true; }
}
