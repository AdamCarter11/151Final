using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    Vector3 objectScale;
    private int health = 10;
    [SerializeField] private Text healthText;
    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        objectScale = transform.localScale;
        //StartCoroutine(changeSize());
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            transform.up = dir;
        }
        
        if(Spectrum.specVal >= 5){
            health++;
            //healthText.text = "Boss health: " + health;
        }
        
        healthText.text = "Boss health: " + health;
        if(health <= 0){
            Destroy(gameObject);
        }
        //print(objectScale);
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("sword")){
            health--;
        }
    }
    /*
    IEnumerator changeSize(){
        while(true){
            transform.localScale = new Vector3(objectScale.x + Spectrum.specVal, objectScale.y + Spectrum.specVal, objectScale.z);
            yield return new WaitForSeconds(1f);
        }
    }
    */
}
