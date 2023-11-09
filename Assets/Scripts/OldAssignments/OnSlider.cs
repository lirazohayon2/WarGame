using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnSlider : MonoBehaviour
{
    public TextMeshProUGUI sliderText;
    private Slider slider; // is actually THIS
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onChange()
    {
        sliderText.text = ""+slider.value;
    }
}
