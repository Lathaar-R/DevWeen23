using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenagerScript : MonoBehaviour
{
    #region Variables and Properties
    //variaveis privadas
    private static GameMenagerScript instance;

    //variaveis publicas

    //propriedades publicas
    public static GameMenagerScript Instance => instance;

    #endregion

    private void Awake() {
        if(Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
