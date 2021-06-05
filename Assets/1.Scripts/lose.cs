using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class lose : MonoBehaviour
{
    public void playmenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void regresar()
    {
        SceneManager.LoadScene("menu");
    }

}

