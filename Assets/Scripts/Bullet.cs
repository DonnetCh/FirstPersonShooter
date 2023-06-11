using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInParent<Enemy>().Health -= 1;
            Debug.Log("menos1vida");
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "Prop")
        {
            Destroy(gameObject);

        }
        if (collision.gameObject.tag == "Obj")
        {
            
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
