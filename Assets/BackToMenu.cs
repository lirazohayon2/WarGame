using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public void goToMenu()
    {
        Debug.Log("goToMenu");
        Utils.restart();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
