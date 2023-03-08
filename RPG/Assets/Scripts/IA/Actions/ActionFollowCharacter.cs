using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/Follow Character")]
public class ActionFollowCharacter : IAAction
{
    public override void Execute(IAController controller)
    {
        FollowCharacter(controller);
    }

    private void FollowCharacter(IAController controller)
    {
        if(controller.CharacterReference == null)
        {
            return;
        }

        Vector3 directionToCharacter = (controller.CharacterReference.position - controller.transform.position);
        Vector3 direction = directionToCharacter.normalized;
        float distance = directionToCharacter.magnitude;

        if(distance >= 1.50f)
        {
            controller.transform.Translate(direction * controller.SpeedMovement * Time.deltaTime);
        }
    }

}
