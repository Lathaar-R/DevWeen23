using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem gunParticleSystem;
    [SerializeField] private ParticleSystem hitPS;
    [SerializeField] private ContactFilter2D bulletsExplosionFilter;

    private List<Collider2D> colliders = new List<Collider2D>();
    
    
    private void OnParticleCollision(GameObject other) {
        // get the collision events
//        Debug.Log("Hit");
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = gunParticleSystem.GetCollisionEvents(other, collisionEvents);
//         Debug.Log(collisionEvents.Count);

        foreach (ParticleCollisionEvent collisionEvent in collisionEvents)
        {
            // colliders.Clear();
            // Physics2D.OverlapCircle(collisionEvent.intersection, 0.5f, bulletsExplosionFilter, colliders);

            // foreach (Collider2D collider in colliders)
            // {
            //     collider.attachedRigidbody.AddForceAtPosition((collider.transform.position - collisionEvent.intersection)*10, collisionEvent.intersection, ForceMode2D.Impulse);
            // }

            hitPS.transform.position = collisionEvent.intersection;
            hitPS.Emit(10);
        }

        if(other.layer == LayerMask.NameToLayer("ObjP"))
        {
            other.GetComponent<ObjScript>().DealDamage(10);
        }
        
    
    }
}
