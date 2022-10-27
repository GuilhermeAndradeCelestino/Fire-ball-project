using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Script : MonoBehaviour
{
    public GameObject currentTerrain;
    //0- 70 , 1 - 50
    public GameObject[] terrainPrefab;
    GameObject lastTerrain;
    int limitador = 1;

    [Space]
    [Space]

    public GameObject paredePrefab;
    
    int terrainSelector;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AplicarDificuldade();
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
            
            GameObject a = Instantiate(terrainPrefab[terrainSelector], (currentTerrain.transform.position + new Vector3(0, 0, 400)), currentTerrain.transform.rotation);
            lastTerrain = currentTerrain;
            currentTerrain = a;
            limitador = 0;
        }
    }

    //Dificuldade
    /* 0 = sem nada  para modificar / 1 = multiplicador velocidade aumenta para em 0.2 e usa terreno 70 / 
     * 2 = multiplicador velocidade aumenta em 0.3/ 3 = 1 - multiplicador velocidade aumenta para em 0.5
     * 4 = usa terreno 50 / 5 = multiplicador aumenta em 1 */

    void AplicarDificuldade()
    {
        if (Player_Script.dificult == 1)
        {
            terrainSelector = 0;
        }
        else if(Player_Script.dificult == 4)
        {
            terrainSelector = 1;
        }
    }
}
