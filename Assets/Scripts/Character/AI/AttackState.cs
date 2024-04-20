using UnityEngine;

public class AttackState: AIState
{
    public override void Enter(AIStateMachineContext context)
    {
        context.Character.StopMoiving();
    }

    public override void Update(AIStateMachineContext context)
    {
        var character = context.Character;
        var stateMachine = context.StateMachine;

        if (character.Target != null)
        {
            var distanceToTarget = character.GetDistanceToTarget();
            if (distanceToTarget > stateMachine.AIProperties.AttackDistance)
            {
                stateMachine.setState(stateMachine.ChaseState);
                return;
            }
            else if (distanceToTarget > stateMachine.AIProperties.ChaseDistance)
            {
                stateMachine.setState(stateMachine.IdleState);
                return;
            }
            else
            {
                context.AttackController.Attack(character.Target.transform.position); 
            }
        }
    }
}
