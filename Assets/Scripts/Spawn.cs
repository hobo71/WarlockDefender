using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    [SerializeField]
    GameObject[] obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void spawn()
    {
        foreach (GameObject o in obj)
        {
            var start = o.GetComponent<MoveTo>();
            start.start = true;
        }
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject obj in objects)
        {
            var att = obj.GetComponent<TowerAttack>();
            if (att != null)
                att.StartAtt();
        }
    }
}
