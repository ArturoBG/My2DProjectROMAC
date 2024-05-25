using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ChangeScene()
    {
        Debug.Log("Loading level 1");
        StartCoroutine(loadSceneRoutine());
        //SceneManager.LoadScene(1);
    }

    private IEnumerator loadSceneRoutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }

    public void Settings()
    {
        Debug.Log("Settings");
        //load settings scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}