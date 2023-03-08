using UnityEngine;

[CreateAssetMenu(menuName = "IA/Actions/Attack Character")]
public class ActionAttackCharacter : IAAction
{
    public override void Execute(IAController controller)
    {
        Attack(controller);
    }

    private void Attack(IAController controller)
    {
        if (controller.CharacterReference == null)
        {
            return;
        }

        if(controller.CanAttackAgain() == false)
        {
            return;
        }

        if(controller.CharacterInAttackRange(controller.AttackRange))
        {
            if (controller.AttackType == AttackTypes.Assault)
            {
                controller.AssaultAttack(controller.Damage);
            }
            else
            {
                controller.MeleeAttack(controller.Damage);
            }

            controller.UpdateTimeBetweenAttacks();
        }
    }
}
