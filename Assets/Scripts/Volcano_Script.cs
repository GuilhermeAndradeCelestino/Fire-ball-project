using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano_Script : MonoBehaviour
{
    public GameObject fireballPrefab;

    [Space]
    public GameObject spawnFireball;
    public float timeToRespawn;

    public static bool canSpawn;

    [Space]
    public bool spawn;

    private void Awake()
    {
        canSpawn = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        //indica no inicio que pode começar a spawnar 
        //canSpawn = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        //spawn atraves de um botao, função para testes
        if (spawn)
        {
            
            StartCoroutine(LaunchFireball());
            spawn = false;
        }

        //Spawna caso o indicador seja true
        if (canSpawn)
        {
            
            StartCoroutine(LaunchFireball());
            canSpawn = false;
        }
    }



    IEnumerator LaunchFireball()
    {
        //aguarda o tempo setado e spawna a bola de fogo na posição do spawnFireball
        yield return new WaitForSeconds(timeToRespawn);
        
            Instantiate(fireballPrefab, spawnFireball.transform.position, spawnFireball.transform.rotation);
   
        //StartCoroutine(LaunchFireball());
        
        
    }

}
