using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Places the player into the scene
    #region Singleton 

    public static PlayerManager instance;

    void Awake ()
    {

        instance = this;
    }
    #endregion


    public GameObject player;



 
}
