using UnityEngine;

public class IdleState : AStateBehaviour
{

    [SerializeField] private float idleDuration = 2f;
    private float idleTimer = 0f;

    // private EnemyFoV fov;


    public override bool InitializeState()
    {
        return true;
    }

    public override void OnStateStart()
    {
        Debug.Log("IDLE");
    }

    public override void OnStateUpdate()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            AssociatedStateMachine.SetState((int)EOtterState.Wandering); // Transition back to Patroling
        }
        // fov.suspicionLevel = lowerSuspicion(fov.suspicionLevel);
    }

    public override void OnStateEnd()
    {
    }

    public override int StateTransitionCondition()
    {
        // if (fov.FindPlayerTarget() != (int)EOtterState.Invalid)
        // {
        //     return fov.FindPlayerTarget(); 
        // }
        // else if (detection.detected)
        // {
        //     return (int)EOtterState.Alarmed;
        // }
        return (int)EOtterState.Invalid;
    }
}