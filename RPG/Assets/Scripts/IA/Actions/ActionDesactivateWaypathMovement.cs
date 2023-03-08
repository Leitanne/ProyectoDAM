using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/Desactivate Waypoint Movement")]
public class ActionDesactivateWaypathMovement : IAAction
{

    public override void Execute(IAController controller)
    {
        if (controller.EnemyMovement == null)
        {
            return;
        }

        controller.EnemyMovement.enabled = false;
    }
}
