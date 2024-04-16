using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AIStateMachineContext
{
    public readonly Character Character;
    public readonly AIStateMachine StateMachine;
    public readonly AttackController AttackController;

    public AIStateMachineContext(Character character, AIStateMachine stateMachine, AttackController attackController)
    {
        Character = character;
        StateMachine = stateMachine;
        AttackController = attackController;
    }
}
