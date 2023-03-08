using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform newPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.localPosition = newPos.position;
        }
    }
}
