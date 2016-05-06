using UnityEngine;
using System.Collections;

public class ArrowBehaviour : MonoBehaviour {

    public int damage = 10;
    public float speed = 15f;
    public Transform target;

	void Start () {
	
	}
	
	void Update () {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distThisFrame) {
            DoArrowHit();
        }
        else {
            transform.Translate(direction.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
	
	}

    void DoArrowHit() {
        EnemieStats enemy = target.GetComponentInParent<EnemieStats>();
        enemy.ApplyDamage(damage);
        Destroy(gameObject);
    }
}
