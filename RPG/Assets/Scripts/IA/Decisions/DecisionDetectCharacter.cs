using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Decisions/Detect Character")]
public class DecisionDetectCharacter : IADecision
{
    public override bool Decide(IAController controller)
    {
        return DetectCharacter(controller);
    }

    private bool DetectCharacter(IAController controller)
    {
        Collider2D characterDetected = Physics2D.OverlapCircle(controller.transform.position, 
            controller.DetectionRange, controller.CharacterLayerMask);

        if(characterDetected != null)
        {
            controller.CharacterReference = characterDetected.transform;
            return true;
        }

        controller.CharacterReference = null;
        return false;
    }
}
