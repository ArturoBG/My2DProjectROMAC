using UnityEngine;

public class GameManager : MonoBehaviour
{
    ///
    //Player reference
    public GameObject playerPrefab;

    public GameObject enemy1Prefab;

    //int lives
    public Transform playerSpawner;

    public GameObject deathScreen;

    //referencias a enemy spawners

    public bool goalReached = false;

    public MenuManager menuManager;

    public void RestartLevel()
    {
        Debug.Log("Restart Level!");
        //menuManager, load level1
        //instanciar a playerPrefab
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        menuManager.QuitGame();
    }

    public void SpawnEnemies()
    {
    }
}