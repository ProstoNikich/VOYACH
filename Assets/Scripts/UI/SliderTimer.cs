using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    Slider sliderBar;
    float maxTime = 0;

    Player player;
    void Start()
    {
        sliderBar = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        maxTime = player.Timer;
    }

    void Update()
    {
        if (player.Timer > 0)
        {
            sliderBar.value = player.Timer / maxTime;
        }
    }
}
