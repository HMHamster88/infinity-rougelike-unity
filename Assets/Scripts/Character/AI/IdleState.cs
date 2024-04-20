using UnityEngine;

public class IdleState : AIState
{
    public override void Update(AIStateMachineContext context)
    {
        var character = context.Character;
        var stateMachine = context.StateMachine;
        if (character.Target != null && character.GetDistanceToTarget() < stateMachine.AIProperties.ChaseDistance)
        {
            stateMachine.setState(stateMachine.ChaseState);
            return;
        }
        
        if (context.Character.StoppedMovement)
        {
            context.Character.MoveTo((Vector2)character.gameObject.transform.position + RandomEx.Vector2() * stateMachine.AIProperties.IdleWalkDistance);
        }
        
    }
}
