using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the main menu of the game
/// </summary>
public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuObject;
    public GameObject SettingsMenuObject;

    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainGameplay");
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
    
}
