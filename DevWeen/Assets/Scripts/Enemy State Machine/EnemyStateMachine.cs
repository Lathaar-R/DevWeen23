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
    private FallingState fallingState;
    private float healthPoints = 100f;
    private Transform playerTransform;

    private DeadState deadState;
    private float damageCooldown = 0f;


    //Serialized Fields
    [SerializeField] private Rigidbody2D rb;
    [SerializeField][Range(0, 10)] private float damageSpeed;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private ParticleSystem gunParticleSystem;
    [SerializeField] private ParticleSystem muzzleFlashPS;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private ParticleSystem elevatorPS;


    //variaveis publicas

    //propriedades publicas
    public State CurrentState => currentState;
    public WalkingState WalkingState => walkingState;
    public FiringState FiringState => firingState;
    public GrabbedState GrabbedState => grabbedState;
    public FallingState FallingState => fallingState;
    public DeadState DeadState => deadState;
    public Rigidbody2D Rb => rb;
    public float HealthPoints => healthPoints;
    public Transform PlayerTransform => playerTransform;
    public ParticleSystem GunParticleSystem => gunParticleSystem;
    public Transform GunTransform => gunTransform;
    public ParticleSystem MuzzleFlashPS => muzzleFlashPS;
    public ParticleSystem ElevatorPS => elevatorPS;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").transform;
        //gunParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        Transform[] teleportPoints = new Transform[8];

        for (int i = 0; i <= 7; i++)
        {
            teleportPoints[i] = GameObject.Find("Point" + (i + 1)).transform;
        }

        walkingState = new WalkingState(this, teleportPoints);
        firingState = new FiringState(this);
        grabbedState = new GrabbedState(this);
        fallingState = new FallingState(this);
        deadState = new DeadState(this);
        ChangeState(walkingState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == deadState) return;
        if(!GameMenagerScript.Instance.IsRunning) return;
        if (PlayerTransform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            gunSpriteRenderer.flipY = true;
            gunTransform.right = Vector2.left;
        }
        else
        {
            spriteRenderer.flipX = false;
            gunSpriteRenderer.flipY = false;
            gunTransform.right = Vector2.right;
        }

        currentState?.Update();
        damageCooldown -= Time.deltaTime;

        //Check if object is upright and if it is not grabbed
        if ((rb.angularVelocity > 0.01f || rb.angularVelocity < -0.01f) && currentState != grabbedState && currentState != deadState)
        {
            ChangeState(fallingState);
        }


    }

    public IEnumerator Die()
    {

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void ChangeState(State newState)
    {
        if (currentState == newState) return;

        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void TakeDamage(float damage)
    {
        if (healthPoints <= 0 || damageCooldown > 0) return;
        damageCooldown = 0.1f;
        healthPoints -= damage;
        //        Debug.Log("Enemy took damage");

        if (currentState != grabbedState)
            ChangeState(fallingState);

        if (healthPoints <= 0)
        {
            ChangeState(deadState);
            //            Debug.Log("Enemy died");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.relativeVelocity.magnitude);
        if (other.relativeVelocity.magnitude > damageSpeed)
        {

            TakeDamage(40);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(20);
        Rb.AddForceAtPosition((transform.position - other.transform.position).normalized * 100, other.transform.position);
    }
}