using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityOSC;

public class Player : MonoBehaviour
{
    //inspector variables
    [SerializeField] private float speed, jumpForce, checkRadius;
    [SerializeField] private int resetJumps, health;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] ParticleSystem ps;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private GameObject playerBullet;

    //hidden variables
    private float moveInput;
    private Rigidbody2D rb;
    private bool isGrounded;
    private int extraJumps;
    private bool facingRight = false;
    private bool canFire = true;
    private int movePoints;
    private int tempVar = 0;
    private bool isShielded = false;

    //OSC stuff
    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
    public Text countText;
    private int count=0, powerUpCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = resetJumps;

        //OSC stuff
        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd","/unity/bg", count);
    }

    void Update()
    {
        //print(movePoints);
        if(Spectrum.specVal >= 5){
            movePoints++;
        }
        if(health <= 0){
            Destroy(gameObject);
            print("game over");
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded){
            extraJumps = resetJumps;
        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded){
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.S)){
            rb.gravityScale = 7f;
        }
        else{
            rb.gravityScale = 3f;
        }

        if(facingRight && moveInput > 0){
            Flip();
        }
        else if(facingRight == false && moveInput < 0){
            Flip();
        }

        moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;

        foreach(KeyValuePair<string, ServerLog> item in servers){
            if(item.Value.log.Count > 0){
                int lastPacketIndex = item.Value.packets.Count-1;
                countText.text = item.Value.packets [lastPacketIndex].Address.ToString ();
				countText.text += item.Value.packets [lastPacketIndex].Data [0].ToString ();
            }
        }

        //Shooting logic
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(Input.mousePosition.x < playerScreenPoint.x && transform.localScale.x == -1){
            //if mouse is on left side of player
            canFire = true;
        }
        else if(Input.mousePosition.x > playerScreenPoint.x && transform.localScale.x == 1){
            //if mouse is on right side of player
            canFire = true;
        }
        else{
            canFire = false;
        }
        if(Input.GetMouseButtonDown(0) && canFire){
            Fire();
        }
    }

    //function to fire the bullet
    void Fire(){
        Vector3 shootDir = Input.mousePosition;
        shootDir.z = 0.0f;
        shootDir = Camera.main.ScreenToWorldPoint(shootDir);
        shootDir = shootDir-transform.position;
        GameObject bulletInst = Instantiate(playerBullet, transform.position, Quaternion.identity);
        bulletInst.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDir.x * 5, shootDir.y * 5);
        Destroy(bulletInst, 3f);
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("bossBullet")){
            if(isShielded){
                isShielded = false;
            }
            else{
                health--;
                playerHealthText.text = "Player Health: " + health;
            }
            Destroy(other.gameObject);
            ParticleSystem spawnedPs = Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(spawnedPs, 1);
        }
        if(other.gameObject.CompareTag("Shield")){
            Destroy(other.gameObject);
            ShieldPowerUp.objectSpawned = false;
            isShielded = true;
        }
    }
}
