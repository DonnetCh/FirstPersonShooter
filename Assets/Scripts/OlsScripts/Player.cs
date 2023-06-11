using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    private CharacterController charController;
    public int Health = 3;
    public int Coins;
    
  
    
    public float Speed = 5f;
    public float rotationSpeed;
    public Transform SpawnPos;
    public Transform Gun;
    public GameObject Bullet;
    public float bulletForce;
    private float originalStepOffset;
    public float jumpSpeed;
    public float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
       
        charController = GetComponent<CharacterController>();
        originalStepOffset = charController.stepOffset;
    }


    // Update is called once per frame
    void Update()
    {
       
        //Puntaje.text = "Monedas recolectadas: " + Coins.ToString() +"/5";
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float magnitude = Mathf.Clamp01(move.magnitude) * Speed;
        move.Normalize();

        Vector3 velocity = move * magnitude;
        velocity.y = ySpeed;
        charController.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Bullet = Instantiate(Bullet, Gun.transform.position, Gun.transform.rotation);
            Rigidbody Bulletrb = Bullet.GetComponent<Rigidbody>();
            Bulletrb.AddForce(bulletForce * Gun.forward, ForceMode.Impulse);
        }
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = Speed * 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 5f;
        }
        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (charController.isGrounded)
        {
            charController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Input.GetButtonDown("Jump")) 
            {
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            charController.stepOffset = 0;
        }

        if(Health <= 0)
        {
            SceneManager.LoadScene(2);
            Coins = 0;
            
        }
    }

 
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            RecibeDmg();
            Debug.Log("RecibioDano");
        }
        Debug.Log("Entroencollision");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Fall")
        {
            transform.position = SpawnPos.position;

            RecibeDmg();
        }
    }
    void RecibeDmg()
    {
        Health -= 1;
    }

}
