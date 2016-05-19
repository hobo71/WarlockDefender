using UnityEngine;
using System.Collections;

public class ArrowBehaviour : MonoBehaviour {

    public LayerMask enemyLayer;

    public float damage = 10;
    public float speed = 15f;
    public float areaOfEffect = 0f;
    public Transform target;

	void Start () {
	
	}
	
	void Update () {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 ajustTargetPosition = new Vector3(target.position.x, target.position.y + 1f, target.position.z);

        Vector3 direction = ajustTargetPosition - this.transform.localPosition;

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
        if (areaOfEffect > 0f) {
            Collider[] colliders = Physics.OverlapSphere(target.position, areaOfEffect, enemyLayer);
            foreach (var coll in colliders) {
                EnemieStats enemy = coll.gameObject.GetComponentInParent<EnemieStats>();
                enemy.ApplyDamage(damage);
            }
        }
        else {
            EnemieStats enemy = target.GetComponentInParent<EnemieStats>();
            enemy.ApplyDamage(damage);
        }
        Destroy(gameObject);
    }
}
