using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLevels : MonoBehaviour
{
    [SerializeField] int maxLevel = 15;
    public void NextLevel()
    {
        Time.timeScale = 1;
        string[] lvlName = SceneManager.GetActiveScene().name.Split('_');
        int numberLvl = int.Parse(lvlName[lvlName.Length - 1]);
        if (numberLvl == maxLevel) SceneManager.LoadScene("MainScene");
        else
        {
            string nextLvl = "Lvl_" + (numberLvl + 1).ToString();
            SceneManager.LoadScene(nextLvl);
            Time.timeScale = 1;
        }
    }
    private void Awake()
    {
        if (Time.timeScale < 1) Time.timeScale = 1;
    }


    public void LoadLevel_MainScane()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadLevel_1()
    {
        SceneManager.LoadScene("Lvl_1");
    }
    public void LoadLevel_2()
    {
        SceneManager.LoadScene("Lvl_2");
    }
    public void LoadLevel_3()
    {
        SceneManager.LoadScene("Lvl_3");
    }
    public void LoadLevel_4()
    {
        SceneManager.LoadScene("Lvl_4");
    }
    public void LoadLevel_5()
    {
        SceneManager.LoadScene("Lvl_5");
    }
    public void LoadLevel_6()
    {
        SceneManager.LoadScene("Lvl_6");
    }
    public void LoadLevel_7()
    {
        SceneManager.LoadScene("Lvl_7");
    }
    public void LoadLevel_8()
    {
        SceneManager.LoadScene("Lvl_8");
    }
    public void LoadLevel_9()
    {
        SceneManager.LoadScene("Lvl_9");
    }
    public void LoadLevel_10()
    {
        SceneManager.LoadScene("Lvl_10");
    }
    public void LoadLevel_11()
    {
        SceneManager.LoadScene("Lvl_11");
    }
    public void LoadLevel_12()
    {
        SceneManager.LoadScene("Lvl_12");
    }
    public void LoadLevel_13()
    {
        SceneManager.LoadScene("Lvl_13");
    }
    public void LoadLevel_14()
    {
        SceneManager.LoadScene("Lvl_14");
    }
    public void LoadLevel_15()
    {
        SceneManager.LoadScene("Lvl_15");
    }
}
