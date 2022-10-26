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

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            StartCoroutine(LaunchFireball());
            spawn = false;
        }

        if (canSpawn)
        {
            StartCoroutine(LaunchFireball());
            canSpawn = false;
        }
    }

    IEnumerator LaunchFireball()
    {
        
        yield return new WaitForSeconds(timeToRespawn);
  
            Instantiate(fireballPrefab, spawnFireball.transform.position, spawnFireball.transform.rotation);
   
        //StartCoroutine(LaunchFireball());
        
        
    }

}
