using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    private BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    public void TurnOnCollider()
    {
        collider.enabled = true;
    }

    public void TurnOffCollider()
    {
        collider.enabled = false;
    }
}