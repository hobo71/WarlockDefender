using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBarEnemyFollow : MonoBehaviour {

	[SerializeField] RectTransform lifeBar;
	private GameObject enemy;

	void Start () {
	
	}

	void Update()
	{
		if (!enemy) {
			Destroy (gameObject);
		} else {
			
			gameObject.transform.position = enemy.GetComponent<Transform> ().position;
			gameObject.transform.localPosition += new Vector3 (0, 14, 0);
			lifeBar.localScale = new Vector3 (1.0f * enemy.GetComponent<EnemieStats> ().life / 100.0f, 1.0f, 1.0f); 
		}
	}

	public void setFollowedEnemy(GameObject newEnemy) {
		enemy = newEnemy;
	}
}
