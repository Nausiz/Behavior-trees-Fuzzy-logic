using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviorTree : NPC
{
    private BehaviorTree behaviorTree;


    void Update()
    {
        agent.SetDestination(enemy.transform.position);
        ShootIfPossible();
    }
}
