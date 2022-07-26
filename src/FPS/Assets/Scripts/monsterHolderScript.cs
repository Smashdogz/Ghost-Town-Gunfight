using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class monsterHolderScript : MonoBehaviour
{
    public int monsterDeathNumber;

//Keeps track of the amount of monsters that have died
//If the amount of monsters reaches the specified amount 
//it will load the victory scene


    void Start()
    {
        monsterDeathNumber = 0;
    }

    void Update()
    {
        if (monsterDeathNumber >= 9)
        {
            Cursor.lockState = CursorLockMode.None; 
            SceneManager.LoadScene("Victory");
        }

        Debug.Log(monsterDeathNumber);
    }

    public void MonsterCount()
    {
        monsterDeathNumber++;
    }
}