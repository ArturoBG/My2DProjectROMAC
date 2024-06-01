using UnityEngine;
using UnityEngine.Events;

public class EnemyFOV : MonoBehaviour
{
    public UnityEvent playerFound;
    public UnityEvent playerLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("collision with " + collision.name);
            //encontramos a player
            // ataca
            playerFound.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("collision with " + collision.name);
            //perdimos a player
            // resume tu patrolling
            playerLost.Invoke();
        }
    }
}