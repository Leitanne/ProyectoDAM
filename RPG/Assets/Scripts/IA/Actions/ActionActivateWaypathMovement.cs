using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/Activate Waypoint Movement")]
public class ActionActivateWaypathMovement : IAAction
{
    public override void Execute(IAController controller)
    {
        if (controller.EnemyMovement == null)
        {
            return;
        }

        controller.EnemyMovement.enabled = true;
    }
}
