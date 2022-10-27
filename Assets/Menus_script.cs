using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus_script : MonoBehaviour
{
    public GameObject[] menus;
    public static int idMenuAtual;
    // Update is called once per frame
    private void Awake()
    {
        idMenuAtual = 0;
    }

    void Update()
    {
        ControleMenus();
    }

    void ControleMenus()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == idMenuAtual)
            {
                menus[idMenuAtual].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }
}
