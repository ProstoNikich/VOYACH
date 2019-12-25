using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinarTimer : MonoBehaviour
{
    Image timerBar;
    float maxTime = 0;

    Player player;

    void Start()
    {
        timerBar = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        maxTime = player.Timer;
    }

    void Update()
    {
        if(player.Timer > 0)
        {
            timerBar.fillAmount = player.Timer / maxTime;
        }
    }
}
