using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {
    [SerializeField]
    GameObject[] particleSpawner;

    public SpawnMonster[] Waves;
    public GameObject[] Spawner;

    private bool start;
    private bool isSpawning;
    private SpawnMonster current;
    private int index;
    private int nbr;
    private int indexSpawner;
    private bool activate;

	// Use this for initialization
	void Start () {
        start = false;
        activate = false;
        isSpawning = false;
        current = null;
        nbr = 0;
        index = -1;
        foreach (GameObject sp in particleSpawner)
        {
            sp.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (start)
        {
            if (!activate)
            {
                foreach (GameObject sp in particleSpawner)
                {
                    sp.SetActive(true);
                }
                activate = true;
            }
            if (index < Waves.Length)
            {
                if (current == null)
                {
                    ++index;
                    current = Waves[index];
                    nbr = current.numberOfEnemies;
                }
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
        current.spawn(Spawner[index].transform, points);
        --nbr;
        isSpawning = false;
    }

    public void startWaves() { start = true; }
}
