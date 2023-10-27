using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    #region Variables and Properties
    //variaveis privadas
    private State currentState;
    private WalkingState walkingState;
    private FiringState firingState;
    private GrabbedState grabbedState;

    //Serialized Fields
    [SerializeField] private Rigidbody2D rb;

    //variaveis publicas

    //propriedades publicas
    private State CurrentState => currentState;
    private WalkingState WalkingState => walkingState;
    private FiringState FiringState => firingState;
    private GrabbedState GrabbedState => grabbedState;
    public Rigidbody2D Rb => rb;

    #endregion

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        walkingState = new WalkingState(this);
        firingState = new FiringState(this);
        grabbedState = new GrabbedState(this);
        ChangeState(walkingState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
