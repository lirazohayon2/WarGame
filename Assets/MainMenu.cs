using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class MainMenu : MonoBehaviour
{
    public static bool mainPlayerTeamDefenders = true;
    public ToggleGroup toggleGroup;


    public void PlayGame()
    {
        Debug.Log("Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    void Start()
    {
        mainPlayerTeamDefenders = true;
    }

    public void updatePlayerOption()
    {
        Debug.Log("UpdatePlayerOption");
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        Debug.Log(toggle.name+ "____" + GetComponentInChildren<Text>().text);
        if (string.Compare(toggle.name, "Def") == 0)
            mainPlayerTeamDefenders = true;
        else
            mainPlayerTeamDefenders = false;
        Debug.Log("mainPlayerTeamDefenders: " + mainPlayerTeamDefenders);
    }

    public void ExitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
         //Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
