using TMPro;
using UnityEngine;

public class AnimationText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDamage;

    public void DamageTextWrite(float amount, Color color)
    {
        textDamage.text = amount.ToString();
        textDamage.color = color;
    }
}
