using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour {

    public float waveTime = 30f;
    public Text text;

    private LevelManager levelManager;

    void Start () {
        levelManager = GameObject.Find("_SCRIPTS_").GetComponent<LevelManager>();
	}
	
	void Update () {
        if (waveTime > 0f)
        {
            waveTime -= Time.deltaTime;
            text.text = Mathf.RoundToInt(waveTime).ToString();
        }
          

        if (waveTime <= 0f)
        {
            levelManager.ReadyState();
            this.enabled = false;
        }
	}
}
