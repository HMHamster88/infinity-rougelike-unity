using UnityEngine;

public class AIStateMachine : InjectComponentBehaviour
{
    public AIProperties AIProperties;

    [GetComponent]
    private Character character;
    [GetComponent]
    private AttackController attackController;

    private AIStateMachineContext context;
    private AIState currentState;

    public readonly IdleState IdleState = new ();
    public readonly ChaseState ChaseState = new ();
    public readonly AttackState AttackState = new ();

    protected override void OnEnable()
    {
        base.OnEnable();
        context = new AIStateMachineContext(character, this, attackController);
        setState(IdleState);
    }

    private void Update()
    {
        currentState.Update(context);
    }

    public void setState(AIState state)
    {
        if (currentState != null)
        {
            currentState.Exit(context);
        }
        currentState = state;
        if (currentState != null) 
        {
            currentState.Enter(context);
        }
    }
}
