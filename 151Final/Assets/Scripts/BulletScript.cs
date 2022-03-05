using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){
            //Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Ground")){
            ParticleSystem spawnedPs = Instantiate(ps, transform.position, Quaternion.identity);
            //Destroy(spawnedPs, 1);
            Destroy(gameObject);
            
        }
    }
}
