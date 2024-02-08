using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isGrabbed = false;
    bool released = false;
    float hp = 100f;

    public Transform grabberTransform;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(grabberTransform != null)
        {

            rb.velocity = ((Vector2)grabberTransform.position - rb.position) * 5;
            released = true;

            if(!isGrabbed){
                rb.excludeLayers = LayerMask.GetMask("Player");
                gameObject.layer = LayerMask.NameToLayer("ObjP");
            }
        }
        else if(released)
        {
            released = false;
            rb.velocity *= 2f;
            isGrabbed = false;
        }
        else
        {
            if(rb.velocity.magnitude < 0.1f)
            {
                gameObject.layer = LayerMask.NameToLayer("Objs");
                rb.excludeLayers = LayerMask.GetMask("Player", "Enemy");
            }
        }

        
    }

    public void DealDamage(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            //GameMenagerScript.Instance.PlayAudio("crash");
            //GameMenagerScript.Instance.PlayAudio("crash");
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other) {
        //rb.AddForceAtPosition(new Vector2(-(other.transform.position - transform.position).x, 0) * 0.7f, other.transform.position, ForceMode2D.Impulse);   
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.sqrMagnitude);
        if(collision.relativeVelocity.sqrMagnitude > 100)
        {
            GameMenagerScript.Instance.PlayAudio("thump");
            DealDamage(UnityEngine.Random.Range(10f, 20f));
        }
    }
}
