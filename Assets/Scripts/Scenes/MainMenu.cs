using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the main menu of the game
/// </summary>
public class MainMenu : MonoBehaviour
{


    public GameObject _mainMenuObject;
    public GameObject _settingsMenuObject;


    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainGameplay");
    }
    public void OnClickOptions()
    {
        _mainMenuObject.SetActive(false);
        _settingsMenuObject.SetActive(true);
    }
    public void BackMainMenu()
    {
        _mainMenuObject.SetActive(true);
        _settingsMenuObject.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
}
