using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void playmenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void opciones()
    {
        SceneManager.LoadScene("options");
    }
        
    public void regresar()
    {
        SceneManager.LoadScene("menu");
    }
    
    public void salir()
    {
        Application.Quit();
    }
}
