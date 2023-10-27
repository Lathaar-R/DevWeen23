using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    #region Variables and Properties
    //variaveis privadas
    private static InputScript instance;
    private Controls gameControls;

    //variaveis publicas


    //propriedades publicas
    public static InputScript Instance => instance;
    public Controls GameControls => gameControls;

    #endregion

    private void Awake() {
        if(Instance == null)
        {
            gameControls = new Controls();
            gameControls.Enable();
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
