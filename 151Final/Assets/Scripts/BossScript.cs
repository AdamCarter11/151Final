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
    public static float activeDist = 9.5f;
    [SerializeField] private AudioClip bossMusic, backgroundMusic;
    [SerializeField] private AudioSource audioSourceVal;
    public static float dist;
    private bool changeMusic = true;
    public static bool bossDead = false;
    private Animator anim;

    //[HideInInspector] public static Vector2 dirToShoot;
    // Start is called before the first frame update
    void Start()
    {
        objectScale = transform.localScale;
        anim = GetComponent<Animator>();
        //StartCoroutine(changeSize());
    }

    
    // Update is called once per frame
    void Update()
    {
        //print(Spectrum.specVal);
        if(target != null && !Scales.playerWin){
            Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            //dirToShoot = dir;
            transform.up = dir;
            dist = Vector2.Distance(target.transform.position, transform.position);
            if(dist < activeDist && changeMusic){
                /*
                audioSourceVal.clip = bossMusic;
                audioSourceVal.Play();
                changeMusic = false;
                */
                AudioManager.instance.SwapTrack(bossMusic);
                changeMusic = false;
            }
            else if(dist >= activeDist && changeMusic == false){
                /*
                audioSourceVal.clip = backgroundMusic;
                audioSourceVal.Play();
                changeMusic = true;
                */
                AudioManager.instance.SwapTrack(backgroundMusic);
                changeMusic = true;
            }
        }
        
        if(Spectrum.specVal >= 10 && AudioManager.instance.isPlayingTrack){
            health++;
            //healthText.text = "Boss health: " + health;
        }
        
        healthText.text = "Boss health: " + health;
        if(health <= 0){
            bossDead = true;
            Scales.playerWin = true;
            transform.rotation = Quaternion.identity;
            anim.Play("Explosion");
            Destroy(gameObject, .7f);
        }
        //print(objectScale);
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("sword")){
            health--;
        }
        if(other.gameObject.CompareTag("playerBullet")){
            health--;
            Destroy(other.gameObject);
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
