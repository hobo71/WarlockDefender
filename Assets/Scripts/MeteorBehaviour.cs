using UnityEngine;
using System.Collections;

public class MeteorBehaviour : MonoBehaviour
{
    public ParticleSystem MeteorExplosionParticleSystem;

    public ParticleSystem MeteorShrapnelParticleSystem;

    public float meteorImpulse = 30f;

    public float damage = 70f;

    public void Start()
    {
        //GetComponent<Rigidbody>().AddForce(-transform.up * meteorImpulse, ForceMode.Impulse);
    }

    public void Update()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * 10f, ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
            col.gameObject.GetComponent<PlayerStats>().ApplyDamage(damage);
        else if (col.gameObject.tag == "Map")
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if (r == null)
            {
                return;
            }
            Vector3 pos, normal;
            if (col.contacts.Length == 0)
            {
                pos = gameObject.transform.position;
                normal = -pos;
            }
            else
            {
                pos = col.contacts[0].point;
                normal = col.contacts[0].normal;
            }

            MeteorExplosionParticleSystem.transform.position = pos;
            MeteorExplosionParticleSystem.transform.rotation = Quaternion.LookRotation(normal);
            MeteorExplosionParticleSystem.Emit(UnityEngine.Random.Range(10, 20));
            MeteorShrapnelParticleSystem.transform.position = col.contacts[0].point;
            MeteorShrapnelParticleSystem.Emit(UnityEngine.Random.Range(10, 20));

            gameObject.GetComponent<Collider>().enabled = false;

            Destroy(r);

            Destroy(gameObject, 1.0f);
        }
    }
}
