using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerStartScript : MonoBehaviour
{
    public static bool first_time_defend = true;
    // Start is called before the first frame update
    void Start()
    {
        bool mainPlayerTeamDefenders = MainMenu.mainPlayerTeamDefenders;
        Debug.Log("MainMenu:   " + mainPlayerTeamDefenders + "__");

        
        if (mainPlayerTeamDefenders)
            setParametersDefendTeam();
        else
            setParametersAttackTeam();
        
    }

    private void setParametersDefendTeam()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] def_players = GameObject.FindGameObjectsWithTag("player_def");

        GameObject player_def1 = def_players[0];
        GameObject player_def2 = def_players[1];

        if (first_time_defend)
        {
            player.transform.position = new Vector3(80f, 44f, -440f);
            first_time_defend = false;
        }
        else
        {
            //setPosition
            float x = (player_def1.transform.position.x + player_def2.transform.position.x) / 2;
            float z = (player_def1.transform.position.z + player_def2.transform.position.z) / 2;
            float y = player.transform.position.y;
            player.transform.position = new Vector3(x, y, z);
        }


        player.layer = LayerMask.NameToLayer("def_group");
        Debug.Log("Change Layer to Player " + player.name + " to layer " + player.layer);

        GameObject mainCameraObject = GameObject.Find("Main Camera");
        GameObject playerGun = Utils.FindChildWithTag(mainCameraObject, "gun");
        mainCameraObject.layer = LayerMask.NameToLayer("def_group");
        playerGun.layer = LayerMask.NameToLayer("def_group");

        //change who is enemy
        playerGun.GetComponent<MainPlayerShooting>().whatIsEnemy = LayerMask.GetMask("atk_group");

    }

    private void setParametersAttackTeam()
    {
        GameObject player = GameObject.FindWithTag("Player");
        GameObject[] atk_players = GameObject.FindGameObjectsWithTag("player_atk");

        GameObject player_atk1 = atk_players[0];
        GameObject player_atk2 = atk_players[1];

        //setPosition
        float x = (player_atk1.transform.position.x + player_atk2.transform.position.x) / 2;
        float z = (player_atk1.transform.position.z + player_atk2.transform.position.z) / 2;
        float y = player.transform.position.y;
        player.transform.position = new Vector3(x, y, z);

        player.layer = LayerMask.NameToLayer("atk_group");
        Debug.Log("Change Layer to Player " + player.name + " to layer " + player.layer);

        GameObject mainCameraObject = GameObject.Find("Main Camera");
        GameObject playerGun = Utils.FindChildWithTag(mainCameraObject, "gun");
        mainCameraObject.layer = LayerMask.NameToLayer("atk_group");
        playerGun.layer = LayerMask.NameToLayer("atk_group");

        //change who is enemy
        playerGun.GetComponent<MainPlayerShooting>().whatIsEnemy = LayerMask.GetMask("def_group");

    }




}
