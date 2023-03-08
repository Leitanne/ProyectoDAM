using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisions/Character Attack Range")]
public class DecisionCharacterAttackRange : IADecision
{
    public override bool Decide(IAController controller)
    {
        return InAttackRange(controller);
    }

    private bool InAttackRange(IAController controller)
    {
        if(controller.CharacterReference == null)
        {
            return false;
        }

        float distance = (controller.CharacterReference.position - controller.transform.position).sqrMagnitude;

        if(distance < Mathf.Pow(controller.AttackRange, 2))
        {
            return true;
        }

        return false;
    }
}
