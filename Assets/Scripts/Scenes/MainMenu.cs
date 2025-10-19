using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the main menu of the game
/// </summary>
public class MainMenu : MonoBehaviour
{


    public GameObject MainMenuObject;
    public GameObject SettingsMenuObject;

    public Animator Animator;
    public GameObject FadeImage;
    private void Awake()
    {
        StartCoroutine(GoToMenu());
    }

    public void OnClickPlay()
    {
        StartCoroutine(GoToMainGame());
    }
    public void OnClickOptions()
    {
        MainMenuObject.SetActive(false);
        SettingsMenuObject.SetActive(true);
    }
    public void BackMainMenu()
    {
        MainMenuObject.SetActive(true);
        SettingsMenuObject.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
    private IEnumerator GoToMainGame()
    {
        FadeImage.SetActive(true);
        Animator.SetTrigger("StartGame");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainGameplay");
    }
    private IEnumerator GoToMenu()
    {
        Animator.SetTrigger("StartMenu");
        yield return new WaitForSeconds(3f);
        FadeImage.SetActive(false);
    }
}
