using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabberScript : MonoBehaviour
{
    #region Variables and Properties
    //variaveis privadas
    private Vector2 grabPoint;
    private bool grabbing;
    private bool grabbingOnEnemy;
    private GrabbedState grabbedState;
    private ObjScript objScript;
    private CircleCollider2D circleCollider;
    private float grabTimer;


    //variaveis serializadas
    [SerializeField] private Transform grabberTransform;
    [SerializeField] private Transform returnTransform;
    [SerializeField] private AnimationCurve grabCurve;
    [SerializeField] private AnimationCurve recoverCurve;
    [SerializeField] private float speed = 25f;
    [SerializeField] private float grabMaxDistance = 5f;

    //variaveis publicas

    //propriedades publicas
    public Transform GrabberTransform => grabberTransform;

    #endregion

    private void OnEnable()
    {
        InputScript.Instance.GameControls.Player.Mouse1.performed += onMouse1Performed;
        InputScript.Instance.GameControls.Player.Mouse1.canceled += onMouse1Performed;
    }

    private void OnDisable()
    {
        InputScript.Instance.GameControls.Player.Mouse1.performed -= onMouse1Performed;
        InputScript.Instance.GameControls.Player.Mouse1.canceled -= onMouse1Performed;
    }



    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    // Update is called once per frame


    void Update()
    {
        if (grabbing)
        {
            grabPoint = Mouse.current.position.ReadValue();
            //tranform the mouse position to world position
            grabPoint = Camera.main.ScreenToWorldPoint(grabPoint);
            // limit the distance to the maximum radius
            grabPoint = returnTransform.position + Vector3.ClampMagnitude((Vector2)(grabPoint - (Vector2)returnTransform.position), grabMaxDistance);

            //make the grabber move to the grapPoint fallowing using the curve as the easing function
            float distance = Mathf.InverseLerp(0, Vector2.Distance(grabberTransform.position, grabPoint), Vector2.Distance(grabberTransform.position, transform.position));
            float step = speed * Time.deltaTime * grabCurve.Evaluate(distance);
            grabberTransform.position = Vector2.MoveTowards(grabberTransform.position, grabPoint, step);
            //test if its near the grabPoint
            // if(!grabbingOnEnemy && Vector2.Distance(grabberTransform.position, grabPoint) < 0.1f)
            // {
            //     grabbing = false;
            // }

            grabTimer -= Time.deltaTime;
            if (grabTimer <= 0 && grabbingOnEnemy)
            {
                grabbing = false;
                if (grabbedState != null) grabbedState.GrabberTransform = null;
                if (objScript != null) objScript.grabberTransform = null;
                grabbingOnEnemy = false;
                grabbing = false;
                circleCollider.enabled = false;
            }
        }
        else
        {
            if (grabbedState != null) grabbedState.GrabberTransform = null;
            //make the grabber move to the original position using the curve as the easing function
            float distance = Mathf.InverseLerp(0, Vector2.Distance(grabberTransform.position, returnTransform.position), Vector2.Distance(grabberTransform.position, grabPoint));
            float step = speed * Time.deltaTime * recoverCurve.Evaluate(distance);
            grabberTransform.position = Vector2.MoveTowards(grabberTransform.position, returnTransform.position, step);
        }
    }

    #region Callbacks

    private void onMouse1Performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            grabbing = true;
            circleCollider.enabled = true;

        }
        else if (obj.canceled)
        {
            grabbing = false;
            if (grabbedState != null) grabbedState.GrabberTransform = null;
            if (objScript != null) objScript.grabberTransform = null;
            grabbingOnEnemy = false;
            grabbing = false;
            circleCollider.enabled = false;
        }
    }

    public void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (grabbingOnEnemy) return;
        if (collision.CompareTag("Enemy"))
        {
            var esm = collision.GetComponent<EnemyStateMachine>();
            if (esm.CurrentState == esm.DeadState) return;
            esm.ChangeState(esm.GrabbedState);
            grabbedState = esm.GrabbedState;
            grabbingOnEnemy = true;
            grabTimer = 5;
        }
        else if (collision.CompareTag("Obj"))
        {
            collision.GetComponent<ObjScript>().grabberTransform = grabberTransform;
            objScript = collision.GetComponent<ObjScript>();
            grabbingOnEnemy = true;
            grabTimer = 5;
        }
    }

    #endregion
}
