using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_bar : MonoBehaviour
{
    [SerializeField] Player Player;
    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    void Update()
    {
        //myText.text = "HP:" + Player.HP + " DMG:" + Player.DMG; //такая запись не продуктивна (конкатинация строк)
    }
}
