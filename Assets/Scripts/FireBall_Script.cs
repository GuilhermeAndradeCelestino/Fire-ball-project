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
        //seta o atk do player false para evitar erros
        Player_Script.atk = false;

        //checa se o valor de gravidade dado é positivo , se for ja converte ele para negativo
        if (gravidade > 0)
        {
            gravidade *= -1;
        }

        //Physics.gravity = new Vector3(0, gravidade, 0);

        //para qualquer força que esteja sendo feita sobre a bola de fogo e adiciona uma forca vertical
        rb.Sleep();
        rb.AddForce(transform.up * force, ForceMode.Impulse);
    }

   

    // Update is called once per frame
    void Update()
    {

        Hit();
    }

    private void FixedUpdate()
    {
       // Hit();
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


    //destroi a bola e indica oara o volcao que pode spawnar outra 
    void DestroyBall(float time)
    {
        Volcano_Script.canSpawn = true;
        Destroy(gameObject, time);
    }


    void Hit()
    {
            // se o player tiver atacando , para qualquer forca que esteja sobre a bola e adiciona uma forca que aumenta conforme 
            //o player carrega o seu ataque
            if (Player_Script.atk)
            {
                rb.Sleep();

                rb.AddForce(new Vector3(0, 0, 1) * atkPower * 10, ForceMode.Impulse);

                //yield return new WaitForSeconds(0.2f);
                //StartCoroutine(moving());
            }
        
    }

    

}
