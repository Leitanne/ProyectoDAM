using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/State")]
public class IAState : ScriptableObject
{
    public IAAction[] actions;
    public IATransition[] transitions;

    public void ExecuteState(IAController controller)
    {
        ExecuteActions(controller);
        DoTransitions(controller);
    }

    private void ExecuteActions(IAController controller)
    {
        if(actions == null || actions.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Execute(controller);
        }
    }

    private void DoTransitions(IAController controller)
    {
        if(transitions == null || transitions.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionValue = transitions[i].Decision.Decide(controller);
            if(decisionValue)
            {
                controller.ChangeState(transitions[i].TrueState);
            }
            else
            {
                controller.ChangeState(transitions[i].FalseState);
            }
        }
    }
}
