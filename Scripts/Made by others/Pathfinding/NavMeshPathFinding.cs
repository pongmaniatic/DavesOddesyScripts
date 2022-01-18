
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshPathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
   
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.tag = "Player";
    }

    public void SetTargetDestination(Vector3 targetDestination)
    {
        agent.destination = targetDestination;
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }
}
