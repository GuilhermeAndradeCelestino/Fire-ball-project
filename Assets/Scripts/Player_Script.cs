using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    public static bool atk;
    public float power;
    public Slider powerUi;

   


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


    }

    private void FixedUpdate()
    {
        PlayerInput();
        UpdateUI();

        
    }

    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (power <= 10)
            {
                power += 0.1f;

            }


        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            
           

            //power = 5;
            FireBall_Script.atkPower = power;
            atk = true;
            //print("Ataquei");
            
        }
        else 
        {
            power = 0;
            
        }

        atk = Input.GetKeyUp(KeyCode.Mouse0);




    }

    void UpdateUI() 
    { 
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
