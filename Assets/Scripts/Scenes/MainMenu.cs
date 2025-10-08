using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the main menu of the game
/// </summary>
public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject _mainMenuObject;
    [SerializeField] GameObject _settingsMenuObject;

    public void OnClickPlay()
    {
        SceneManager.LoadScene("MainGameplay");
    }
    public void OnClickOptions()
    {
        _mainMenuObject.SetActive(false);
        _settingsMenuObject.SetActive(true);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    
}
