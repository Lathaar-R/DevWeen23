using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class WalkingState : State
{
    private float distanceToStop = 0;
    private float speed = 0;
    private bool chasing = false;
    private Transform[] teleportPoints;
    public WalkingState(EnemyStateMachine stateMachine, Transform[] teleportPoints) : base(stateMachine)
    {
        this.teleportPoints = teleportPoints;
    }

    public override void Enter()
    {
        distanceToStop = UnityEngine.Random.Range(7f, 9f);
        speed = UnityEngine.Random.Range(3f, 5f);
        stateMachine.transform.rotation = Quaternion.identity;
        
        foreach(var x in teleportPoints)
        {
            //Debug.Log(x.position);  
        }
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        bool otherSide = false;
        
        var pos = (Vector2)stateMachine.PlayerTransform.position - stateMachine.Rb.position;
        

        var posx = new Vector2(((Vector2)stateMachine.PlayerTransform.position - stateMachine.Rb.position).x, 0);
        var posy = new Vector2(0, ((Vector2)stateMachine.PlayerTransform.position - stateMachine.Rb.position).y);
        var dir = posx.normalized;
        var playerSide = stateMachine.PlayerTransform.position.x > 0 ? 1 : -1;

        //check what level collider the player and the enemy are
        var sameLevel = Physics2D.OverlapPoint(stateMachine.PlayerTransform.position, LayerMask.GetMask("Level")).gameObject == Physics2D.OverlapPoint(stateMachine.transform.position, LayerMask.GetMask("Level")).gameObject;

        if(!sameLevel || playerSide * stateMachine.Rb.position.x < 0)
        {
            //Debug.Log("Mag > 3");
            //get which side of the screen the player is
            
            //and than make the enemy walk to the nearest teleport point on that side, if the enemy is not on that side find the nearest teleport point on that side
            //mesmo lado//Debug.Log
            Transform teleportPoint;
            if(playerSide * stateMachine.Rb.position.x > 0)
            {
                //Debug.Log("Mesmo lado1");
                //get the nearest teleport point
                teleportPoint = teleportPoints.Where(x => x.position.x * playerSide > 0).OrderBy(x => Vector2.Distance(x.position, stateMachine.Rb.position)).First();
            }
            else
            {
                
                //get the nearest teleport point of the enemy only
                teleportPoint = teleportPoints
                    .OrderBy(x => Vector2.Distance(x.position, stateMachine.Rb.position))
                    .First();
                
                //Debug.Log(Array.IndexOf(teleportPoints, teleportPoint));
                if(Array.IndexOf(teleportPoints, teleportPoint) <= 1)
                {
                    teleportPoint = teleportPoints.Where(x => x.position.x * playerSide > 0).OrderBy(x => Vector2.Distance(x.position, stateMachine.Rb.position)).First();
                }

                if(Array.IndexOf(teleportPoints, teleportPoint) > 1)
                {
                    //Debug.Log("Outro lado2");
                    otherSide = true;
                }
                else
                {
                    //Debug.Log("Mesmo lado2");
                    teleportPoint = teleportPoints.Where(x => x.position.x * playerSide > 0).OrderBy(x => Vector2.Distance(x.position, stateMachine.Rb.position)).First();
                }
            }
            //teleportPoint = teleportPoints.Where(x => x.position.x * playerSide > 0).OrderBy(x => Vector2.Distance(x.position, stateMachine.Rb.position)).First();

            dir.x = ((Vector2)teleportPoint.position - stateMachine.Rb.position).normalized.x;

            stateMachine.Rb.velocity = dir * speed;

            //check if the enemy is close enough to the teleport point
            if(Vector2.Distance(stateMachine.Rb.position, teleportPoint.position) < 0.5f)
            {
                //Debug.Log("Teleport");
                //Check if the player in on top or bottom of the enemy
                if(!otherSide && (stateMachine.PlayerTransform.position.y - stateMachine.transform.position.y) > 0)
                {
                    //Debug.Log("Top");
                    //if the player is on top, teleport the enemy to the bottom
                    //get the index of the current teleport point inside teleportPoints
                    var index = Array.IndexOf(teleportPoints, teleportPoint);
                    stateMachine.Rb.position = teleportPoints[index + 2].position;
                    stateMachine.ElevatorPS.Emit(30);
                }
                else
                {
                    //Debug.Log("Bottom");
                    //if the player is on top, teleport the enemy to the bottom
                    //get the index of the current teleport point inside teleportPoints
                    var index = Array.IndexOf(teleportPoints, teleportPoint);
                    stateMachine.Rb.position = teleportPoints[index - 2].position;
                    stateMachine.ElevatorPS.Emit(30);
                }
            }

        }
        else
        {
            if(pos.magnitude > distanceToStop)
            {
                stateMachine.Rb.velocity = dir * speed;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.FiringState);
            }   
        }
    }
}
