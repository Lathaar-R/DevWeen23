using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float healthPoints = 100f;
    private bool isDead = false;
    private MovementScript2D movementScript2D;

    [SerializeField] private GameObject[] smokePS;

    public float HealthPoints => healthPoints;
    public bool IsDead => isDead;


    private void Awake() {
        movementScript2D = GetComponent<MovementScript2D>();
    }

    public void Damage(float damage, Vector3 direction)
    {
        if(isDead) return;
        healthPoints -= damage;
        GameMenagerScript.Instance.PlayAudio("takeDamage");
        //movementScript2D.SetSpeed(direction);
        if(healthPoints <= 0)
        {
            Die();
            GameMenagerScript.Instance.EndGame();
        }
    }

    private void Die()
    {
        
        isDead = true;

        foreach (GameObject ps in smokePS)
        {
            ps.GetComponent<ParticleSystem>().Stop();
        }
    }

    #region Callbacks
    private void OnParticleCollision(GameObject other) {
        //get the direction of the particle
        Vector3 direction = (transform.position - other.transform.position).normalized;
        Damage(10, direction * 5);
    }

    #endregion


}
