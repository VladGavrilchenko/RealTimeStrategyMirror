using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using UnityEngine.InputSystem;

public class UnitMover : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Targeter targeter;
    [SerializeField] private float chaseRange = 10;
    #region Server

    [ServerCallback]
    private void Update()
    {
        Targetable target = targeter.GetTarget();

        if(target != null)
        {
            if ((target.transform.position - transform.position).sqrMagnitude > chaseRange*chaseRange)
            {
                agent.SetDestination(target.transform.position);
            }
            else if (agent.hasPath)
            {
                agent.ResetPath();
            }
            return;
        }


        if(!agent.hasPath) { return; }

        if(agent.remainingDistance > agent.stoppingDistance) { return; }

        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        targeter.ClearTarget();


        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f , NavMesh.AllAreas)) { return; }
        
        agent.SetDestination(hit.position);
    }


    #endregion

}
