using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Script : MonoBehaviour
{


    Rigidbody rb;
    
    public float force;
    public float gravidade;
    
    public static float atkPower;

    bool dontDestroy;



    // Start is called before the first frame update
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dontDestroy = false;
    }

    void Start()
    {   
        //checa se o valor de gravidade dado é positivo , se for ja converte ele para negativo
        if (gravidade > 0)
        {
            gravidade *= -1;
        }

        //Physics.gravity = new Vector3(0, gravidade, 0);
        StartCoroutine(LaunchBall());
    }

   

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "DestroyFireball")
        {
            if (!dontDestroy)
            {
                Player_Script.atirando = false;
                Destroy(gameObject);
            }
            
        }

        if(other.gameObject.tag == "BreakWall")
        {
            dontDestroy = true;
            QuebrarParede_Script.breakPower = atkPower;
            QuebrarParede_Script.quebrar = true;
            Camera_script.shakeCamera = true;
            StartCoroutine(DestroyBall());
        }
    }

    //destroi a bola e indica oara o volcao que pode spawnar outra 
   

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(1.5f);
        Player_Script.atirando = false;
        dontDestroy = false;
        Destroy(gameObject);
    }

    //Lança a bola para cima e depois aplai uma forçã horizontal baseado no quanto o jogador carregou de poder
    IEnumerator LaunchBall()
    {
        Player_Script.isInCooldown = true;
        rb.Sleep();
        rb.AddForce(transform.up * force, ForceMode.Impulse);

        yield return new WaitForSeconds(0.5f);
        print(atkPower);
        rb.Sleep();
        rb.AddForce(new Vector3(0, 0, 1) * atkPower * 7, ForceMode.Impulse);
    }


    

}
