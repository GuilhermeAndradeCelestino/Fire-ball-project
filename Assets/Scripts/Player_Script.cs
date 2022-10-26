using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    public static bool atk;
    public bool usarSpace;
    
    
    public float power;
    public GameObject powerUi;
    public Animator powerUiAnim;
    
    bool charging;
    bool usingPowerBar;


    // Start is called before the first frame update
    void Start()
    {
        power = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        print("atk: " + atk);
        print("hit: " + FireBall_Script.hit);

        if (usarSpace)
        {
            PlayerInput(KeyCode.Space);
        }
        else
        {
            PlayerInput(KeyCode.Mouse0);
        }
        

    }

    private void FixedUpdate()
    {
       
        UpdateUI();
        
        if (charging)
        {
            if (power <= 10)
            {
                power += 0.1f;

            }
        }
    }


    void PlayerInput(KeyCode tecla)
    {
        //quando o jogador clikar e segurar = começa a carregar
        //ao soltar o mouse indica para a bola a força de arremeço e que ela pode ser lançada
        //depois disso zera o poder



        if (Input.GetKey(tecla))
        {
            charging = true;
            usingPowerBar = true;
        }
        else if (Input.GetKeyUp(tecla))
        {
            charging = false;
           

            //power = 5;
            FireBall_Script.atkPower = power;
            //atk = true;
            print("Ataquei");
            
        }
        else 
        {
            power = 0;
            usingPowerBar = false;
        }

        atk = Input.GetKeyUp(tecla);




    }

    void UpdateUI() 
    {
        //atualiza a ui
        if (usingPowerBar)
        {
            powerUiAnim.SetBool("Aparecer", true);
            powerUiAnim.SetTrigger("Comecar");
        }
        else if(!usingPowerBar)
        {
            powerUiAnim.SetBool("Aparecer", false);
        }
        powerUi.GetComponent<Slider>().value = power;
    }

    


    
}
