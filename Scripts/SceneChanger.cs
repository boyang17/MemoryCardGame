using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ToGameScene()
    {
        SceneManager.LoadScene("Scene");
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void ToCongratsScene()
    {
        SceneManager.LoadScene("Congrats");
    }
}
