using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Script : MonoBehaviour
{
    public GameObject currentTerrain;
    public GameObject terrainPrefab;
    GameObject lastTerrain;
    int limitador = 1;

    [Space]
    [Space]

    public GameObject paredePrefab;
    //1- , 2- , 3-
    public static int dificult;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Terrain(other);
    }

    // Reposponsavel por gerar novos terrenos e destruir os antigos
    void Terrain(Collider o)
    {
        //Destruir ultimo terreno
        if (o.gameObject.tag == "DestroyLastTerrain")
        {
            Destroy(lastTerrain, 1);
            limitador = 1;
        }

        //Gerar novo terreno
        if (o.gameObject.tag == "GenerateNewGround" && limitador == 1)
        {
            GameObject a = Instantiate(terrainPrefab, (currentTerrain.transform.position + new Vector3(0, 0, 400)), currentTerrain.transform.rotation);
            lastTerrain = currentTerrain;
            currentTerrain = a;
            limitador = 0;
        }
    }

}
