using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChaseState: AIState
{
    public override void Enter(AIStateMachineContext context)
    {
        var character = context.Character;
        character.StopMoiving();
        if (character.Target != null)
        {
            character.MoveTo(character.Target.transform.position);
        }
    }

    public override void Update(AIStateMachineContext context)
    {
        var character = context.Character;
        var stateMachine = context.StateMachine;

        if (character.Target != null)
        {
            var distanceToTarget = character.GetDistanceToTarget();
            if (distanceToTarget > stateMachine.AIProperties.ChaseDistance) 
            {
                stateMachine.setState(stateMachine.IdleState);
                return;
            }
            else if (distanceToTarget < stateMachine.AIProperties.AttackDistance)
            {
                stateMachine.setState(stateMachine.AttackState);
                return;
            }
        }

        if (context.Character.StoppedMovement)
        {
            context.Character.MoveTo(character.Target.transform.position);
        }
    }
}
