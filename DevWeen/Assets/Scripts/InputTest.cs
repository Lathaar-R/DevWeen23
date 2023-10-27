using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private void OnEnable() {
        InputScript.Instance.GameControls.Player.Movement.performed += OnMovementPerformed;   
    }

    private void OnDisable() {
        InputScript.Instance.GameControls.Player.Movement.performed -= OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }
}
