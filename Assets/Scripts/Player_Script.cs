using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    public static bool atk;
    public float power;
    public Slider powerUi;
    bool charging;
   


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
        PlayerInput();

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


    void PlayerInput()
    {
        //quando o jogador clikar e segurar = começa a carregar
        //ao soltar o mouse indica para a bola a força de arremeço e que ela pode ser lançada
        //depois disso zera o poder



        if (Input.GetKey(KeyCode.Space))
        {
            charging = true;


        }
        else if (Input.GetKeyUp(KeyCode.Space))
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
            
        }

        atk = Input.GetKeyUp(KeyCode.Space);




    }

    void UpdateUI() 
    { 
        //atualiza a ui
        powerUi.value = power;
    }

    IEnumerator a()
    {
        yield return new WaitForSeconds(1f);
        power = 0;

        if (atk && !FireBall_Script.hit)
        {
            atk = false;
        }
    }
}
