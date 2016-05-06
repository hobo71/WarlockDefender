using UnityEngine;
using System.Collections;

public class MeteorBehaviour : MonoBehaviour
{
    public ParticleSystem MeteorExplosionParticleSystem;
    public ParticleSystem MeteorShrapnelParticleSystem;

    public float meteorImpulse = 30f;
    public float damage = 70f;
    public float lifespan = 5f;

    public void Start()
    {
        //GetComponent<Rigidbody>().AddForce(-transform.up * meteorImpulse, ForceMode.Impulse);
    }

    public void Update()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * 10f, ForceMode.Impulse);

        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
            Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            col.gameObject.GetComponent<PlayerStats>().ApplyDamage(damage);
        if (col.gameObject.tag == "Enemy")
            col.gameObject.GetComponent<EnemieStats>().ApplyDamage((int)damage);
        if (col.gameObject.tag == "Map")
        {
            Renderer r = gameObject.GetComponent<Renderer>();
            if (r == null)
            {
                return;
            }

            MeteorExplosionParticleSystem.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            MeteorExplosionParticleSystem.transform.rotation = Quaternion.LookRotation(-transform.position);
            MeteorExplosionParticleSystem.Emit(UnityEngine.Random.Range(10, 20));
            MeteorShrapnelParticleSystem.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            MeteorShrapnelParticleSystem.Emit(UnityEngine.Random.Range(10, 20));

            gameObject.GetComponent<Collider>().enabled = false;

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip, transform.localScale.x);
            Destroy(r);

            Destroy(gameObject, 1.0f);
        }
    }
}
