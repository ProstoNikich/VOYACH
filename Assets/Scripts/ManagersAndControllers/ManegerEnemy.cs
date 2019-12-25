using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManegerEnemy : MonoBehaviour
{
    [SerializeField] GameObject panelNextLevel;
    List<Enemy> enemies = new List<Enemy>();
    static public ManegerEnemy instanse;
    private void Awake()
    {
        instanse = this;
    }


    void Update()
    {
        if (enemies.Count == 0)
        {
            panelNextLevel.SetActive(true);
            Time.timeScale = 0;
        }
       
    }

    public void AddMy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void DeleteMy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
