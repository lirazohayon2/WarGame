using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for Text

public class PickCoin : MonoBehaviour
{
    public AudioSource pickSound; // must be connected in Unity
    public Text coinsText;
    public Text info;
    public static int numCoins = 0;
    // Start is called before the first frame update
    void Start()
    {
        info.text = "You need to collect 6 coins";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // eliminate object (coin)
        this.gameObject.SetActive(false);
        pickSound.Play();
        numCoins++;
        coinsText.text = "Gold Coins: " + numCoins;
        if (numCoins >= 3)
        {
            //info.text = "";
            info.gameObject.SetActive(false);
        }
    }
}
