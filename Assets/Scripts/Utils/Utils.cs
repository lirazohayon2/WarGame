using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Utils 
{
    private static bool isAlreadyAnouncedWinningTeam = false;

    private static string [] arrUILogKills = new string[4];
    public static void restart()
    {
        isAlreadyAnouncedWinningTeam = false;
        arrUILogKills = new string[4];
    }
    public static GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;
        //Debug.Log("Tag: " + tag);
        //Debug.Log("parent: " + parent.name);
        foreach (Transform transform in parent.transform)
        {
            //Debug.Log("transform.tag: " + transform.tag);
            //Debug.Log("transform.name: " + transform.name);
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }

    public static void dieAct(GameObject target)
    {
        bool mainPlayer = false;
        GameObject mainPlayerObject = target;
        if (string.Compare(target.name, "MainPlayer") == 0) {
            mainPlayerObject = target;
            target = target.transform.Find("Main Camera").gameObject;
            mainPlayer = true;
        }

        //Debug.Log("target " + target);
        //Debug.Log("find gun" + FindChildWithTag(target, "gun"));
        //Debug.Log("find healthBar" + FindChildWithTag(target, "healthBar"));

        FindChildWithTag(target, "gun").SetActive(false);

        if (!mainPlayer)
        {
            FindChildWithTag(target, "healthBar").SetActive(false);
            target.GetComponent<Animator>().SetInteger("State", 2);
        }

        FindChildWithTag(target, "skull").SetActive(true);

        //Change Layer
        
        int LayerIgnoreRaycast = LayerMask.NameToLayer("dead_group");
        if(mainPlayer)
            mainPlayerObject.layer = LayerIgnoreRaycast;
        target.layer = LayerIgnoreRaycast;
        Debug.Log("Change Layer to Player " + target.name +  " to layer " + target.layer);

        checkIfGameIsOver();
    }

    
    private static void checkIfGameIsOver()
    {
        GameObject[] def_players = GameObject.FindGameObjectsWithTag("player_def");
        GameObject[] atk_players = GameObject.FindGameObjectsWithTag("player_atk");
        GameObject player = GameObject.FindWithTag("Player");




        int count_layer_def = 0;
        int count_layer_atk = 0;

        foreach (GameObject def_player in def_players)
        {
            if (LayerMask.NameToLayer("def_group") == def_player.layer)
                count_layer_def++;
        }

        foreach (GameObject atk_player in atk_players)
        {
            if (LayerMask.NameToLayer("atk_group") == atk_player.layer)
                count_layer_atk++;
        }

        if (player.layer == LayerMask.NameToLayer("def_group"))
            count_layer_def++;
        else if (player.layer == LayerMask.NameToLayer("atk_group"))
            count_layer_atk++;

        Debug.Log("Layer def size: " + count_layer_def);
        Debug.Log("Layer atk size: " + count_layer_atk);

        if (!isAlreadyAnouncedWinningTeam)
        {
            if (count_layer_def == 0)
            {
                announceWinningTeam("Attacking Team");
                isAlreadyAnouncedWinningTeam = true;
            }
            else if (count_layer_atk == 0)
            {
                announceWinningTeam("Defending Team");
                isAlreadyAnouncedWinningTeam = true;
            }

        }
    }

    private static void announceWinningTeam(string win_team_name)
    {
        GameObject gameover = GameObject.FindWithTag("gameover");
        gameover.GetComponent<Text>().text = "Game Over\n"+ win_team_name + " WIN!";
        FindChildWithTag(GameObject.Find("UI"), "backButton").SetActive(true);

    }

    public static void announceAKill(string killerName, string targetName)
    {
        for (int i = 1; i<= arrUILogKills.Length - 1; i++)
            arrUILogKills[i-1] = arrUILogKills[i];
        arrUILogKills[arrUILogKills.Length - 1] = killerName + " killed " + targetName;

        string scoreBoardNewStr = "";
        foreach (string str in arrUILogKills)
            if(str!=null)
                scoreBoardNewStr += str + "\n";
        GameObject scoreBoard = GameObject.FindWithTag("score");
        scoreBoard.GetComponent<Text>().text = scoreBoardNewStr;

    }



}
