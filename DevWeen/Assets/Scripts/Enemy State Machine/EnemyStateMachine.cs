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
    private GameObject player;

    //Serialized Fields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minDistanceToPlayer = 3f;

    //variaveis publicas

    //propriedades publicas
    public State CurrentState => currentState;
    public WalkingState WalkingState => walkingState;
    public FiringState FiringState => firingState;
    public GrabbedState GrabbedState => grabbedState;
    public Rigidbody2D Rb => rb;
    public float Speed => speed;
    public float MinDistanceToPlayer => minDistanceToPlayer;

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
