using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_Script : MonoBehaviour
{
    // 0 - menu principal / 1 - menu tutorial
    

 

    public void PlayButton(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void TutorialButton()
    {
        Menus_script.idMenuAtual = 1;
    }

    public void SairButton()
    {
        Application.Quit();
    }


    public void MenuButton(int sceneID)
    {
        if (SceneManager.GetActiveScene().buildIndex == sceneID)
        {
            Menus_script.idMenuAtual = 0;
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneID);
        }
    }


    public void ReniniciarButton(int sceneID)
    {
        Time.timeScale = 1;
        Player_Script.idHudAtual = 0;
        SceneManager.LoadScene(sceneID);
    }

    public void SairPausaButton()
    {
        Time.timeScale = 1;
        Player_Script.idHudAtual = 0;
    }


}
