using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Script : MonoBehaviour
{


    Rigidbody rb;
    
    public float force;
    public float gravidade;
    public static bool hit;
    public static float atkPower;


    // Start is called before the first frame update
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }


    void Start()
    {
        Player_Script.atk = false;
        if (gravidade > 0)
        {
            gravidade *= -1;
        }

        //Physics.gravity = new Vector3(0, gravidade, 0);
        rb.Sleep();

        rb.AddForce(transform.up * force, ForceMode.Impulse);
    }

   

    // Update is called once per frame
    void Update()
    {

       
    }

    private void FixedUpdate()
    {
        Hit();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DestroyFireball")
        {
            DestroyBall(0);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerLocalHit")
        {
            //print("pode hitar");
            hit = true;
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerLocalHit")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            //print("não pode hitar");
            hit = false;
        }
    }


    void DestroyBall(float time)
    {
        Volcano_Script.canSpawn = true;
        Destroy(gameObject, time);
    }


    void Hit()
    {
        if (Player_Script.atk)
        {
            StartCoroutine(moving());
        }
    }

    IEnumerator moving()
    {
        if (hit)
        {
            // StartCoroutine(slowTime());
            rb.Sleep();

            rb.AddForce(new Vector3(0, 0, 1) * atkPower * 10, ForceMode.Impulse);

            yield return new WaitForSeconds(0.2f);
            //Player_Script.atk = false;
            
        }
    }

}
