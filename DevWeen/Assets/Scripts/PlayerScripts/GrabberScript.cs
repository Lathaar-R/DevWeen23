using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabberScript : MonoBehaviour
{
    #region Variables and Properties
    //variaveis privadas
    private Transform grabberTransform;

    //variaveis publicas

    //propriedades publicas
    public Transform GrabberTransform => grabberTransform;

    #endregion

    private void OnEnable() {
        InputScript.Instance.GameControls.Player.Mouse1.performed += onMouse1Performed;
        InputScript.Instance.GameControls.Player.Mouse1.canceled += onMouse1Performed;
    }

    private void OnDisable() {
        InputScript.Instance.GameControls.Player.Mouse1.performed -= onMouse1Performed;
        InputScript.Instance.GameControls.Player.Mouse1.canceled -= onMouse1Performed;
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Callbacks

    private void onMouse1Performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Mouse 1");
    }

    #endregion
}
