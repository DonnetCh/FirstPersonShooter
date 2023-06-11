using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMov : MonoBehaviour
{
    public Transform Camera;
    public int Health = 20;
    public float moveSpeed;
    public float moveMultiplier;
    public Transform orientation;
    public Transform spawnPos;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public TextMeshProUGUI obj;
    public TextMeshProUGUI vida;
    public TextMeshProUGUI kills;
    
    
    public GameObject[] objectives;
    public int CompletedGame;
    public int CopsKilled;
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatisGround;
    public bool grounded;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public KeyCode jumpKey = KeyCode.Space;
    //weapons
    public bool pistol;
    public GameObject pistolObj;
    public bool shotgun;
    public GameObject shotgunObj;
    public bool smg;
    public GameObject smgObj;

    public bool ableToShoot;

    public GameObject bullet;
    public GameObject perdigon;

    public Transform pistolTrans;

    public Transform shotgunTrans;
    public Transform shotgunTrans1;
    public Transform shotgunTrans2;
    public Transform shotgunTrans3;
    public Transform shotgunTrans4;
    public Transform shotgunTrans5;
    public Transform shotgunTrans6;
    
    public Transform smgTrans;


    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

        obj.text = "Objetivos restantes: "+ objectives.Length.ToString();
        kills.text = "Asesinatos: " + CopsKilled.ToString();
        vida.text = "Vida: " + Health.ToString();
        CheckWeaponActive();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        MyInput();
        SpeedControl();
        WeaponLogic();
        if (grounded)
        {
            rb.drag = groundDrag;
            readyToJump = true;
        }
        else
        {
            rb.drag = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed * moveMultiplier;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5;
        }



        objectives = GameObject.FindGameObjectsWithTag("Obj");
        if (objectives.Length <= 0)
        {
            SceneManager.LoadScene(2);
        }

        if (Health <= 0)
        {
            transform.position = spawnPos.position;
            Health = 20;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = false;
    }

    private void WeaponLogic()
    {
        if (pistol && ableToShoot && Input.GetButtonDown("Fire1"))
        {
            ableToShoot = false;
            GameObject bulletgo = Instantiate(bullet, pistolTrans.position, bullet.transform.rotation);
            bulletgo.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);
        }
        else if (pistol && !ableToShoot && Input.GetButtonUp("Fire1"))
        {
            ableToShoot = true;

        }
        if (shotgun && ableToShoot && Input.GetButtonDown("Fire1"))
        {
            ableToShoot = false;
            GameObject bulletgo = Instantiate(perdigon, shotgunTrans.position, perdigon.transform.rotation);
            bulletgo.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);

            GameObject bulletgo1 = Instantiate(perdigon, shotgunTrans1.position, perdigon.transform.rotation);
            bulletgo1.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);

            GameObject bulletgo2 = Instantiate(perdigon, shotgunTrans2.position, perdigon.transform.rotation);
            bulletgo2.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);

            GameObject bulletgo3 = Instantiate(perdigon, shotgunTrans3.position, perdigon.transform.rotation);
            bulletgo3.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);

            GameObject bulletgo4 = Instantiate(perdigon, shotgunTrans4.position, perdigon.transform.rotation);
            bulletgo4.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);

            GameObject bulletgo5 = Instantiate(perdigon, shotgunTrans5.position, perdigon.transform.rotation);
            bulletgo5.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);

            GameObject bulletgo6 = Instantiate(perdigon, shotgunTrans6.position, perdigon.transform.rotation);
            bulletgo6.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);
           

        }
        else if (shotgun && !ableToShoot && Input.GetButtonUp("Fire1"))
        {
            Invoke("EnableShooting", 1);
        }

        if (smg  && Input.GetButton("Fire1"))
        {
            //ableToShoot = false;
            GameObject bulletgo = Instantiate(bullet, smgTrans.position, bullet.transform.rotation);
            bulletgo.GetComponent<Rigidbody>().AddForce(Camera.transform.forward * bulletSpeed);
        }
     
    }

    private void EnableShooting()
    {
        ableToShoot = true;
    }

    private void CheckWeaponActive()
    {
        if (pistolObj.activeSelf)
        {
            pistol = true;
        }
        else
        {
            pistol = false;
        }
        if (smgObj.activeSelf)
        {
            smg = true;
        }
        else
        {
            smg = false;
        }
        if (shotgunObj.activeSelf)
        {
            shotgun = true;
        }
        else
        {
            shotgun = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("enemyBullet"))
        {
            Health -= 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("enemyBullet"))
        {
            Health -= 3;
        }

        if (other.tag == ("Fall"))
        {
            transform.position = spawnPos.position;
        }
    }
}
