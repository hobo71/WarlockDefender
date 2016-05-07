using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarEnemyFollow : MonoBehaviour {

	[SerializeField] RectTransform lifeBar;
	private bool enemyWasSet = false;
	private GameObject enemy;
	private float maxLife = 100.0f;

	void Start () {
		//gameObject.GetComponent<RectTransform> ().sizeDelta = 

	}

	void Update()
	{
		if (enemyWasSet && !enemy) {
			Destroy (gameObject);
		} else if (enemy) {
			gameObject.transform.position = enemy.GetComponent<Transform> ().position;
			gameObject.transform.position += new Vector3 (0f, 2.5f, 0f);
			lifeBar.localScale = new Vector3 (enemy.GetComponent<EnemieStats> ().life / maxLife, 1.0f, 1.0f); 
		}
	}

	public void setFollowedEnemy(GameObject newEnemy) {
		enemy = newEnemy;
		enemyWasSet = true;
	}

	public void setMaxLifeEnemy(float newMaxLife) {
		maxLife = newMaxLife;
	}
}
