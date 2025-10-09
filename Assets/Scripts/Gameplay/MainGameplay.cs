using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Entry point of the main gameplay
/// Main singleton to get access to everything :
/// Player, Enemies, Data, UI etc...
/// </summary>
public class MainGameplay : MonoBehaviour
{
    #region Singleton

    public static MainGameplay Instance;

    #endregion

    /// <summary>
    /// Represents the current state of gameplay
    /// </summary>
    public enum GameState
    {
        Gameplay,
        GameOver
    }

    


    #region Inspector

    [SerializeField] PlayerController _player;
    [SerializeField] GameplayData _data;
    [SerializeField] GameUIManager _gameUIManager;

    [SerializeField] GameObject _prefabXp;
    [SerializeField] GameObject _superPrefabXp;

    [SerializeField] GameObject _panelForDisplay;
    [SerializeField] TextMeshProUGUI _textComponent;
    [SerializeField] string _textToDisplay;

    #endregion

    #region Properties

    public PlayerController Player => _player;
    public GameObject PrefabXP => _prefabXp;
    public GameObject SuperPrefabXP => _superPrefabXp;
    public GameState State { get; private set; }
    public List<EnemyController> Enemies => _enemies;
    public GameUIManager GameUIManager => _gameUIManager;
    public GameObject PanelForDisplay => _panelForDisplay;
    public TextMeshProUGUI TextComponent  => _textComponent;
    //public AudioSource AudioSource => _audioSource;
    //public AudioClip AudioClip => _audioClip;

    #endregion

    #region Fields

    readonly List<EnemyController> _enemies = new List<EnemyController>();
    float _timerIncrement;
    int _timerSeconds;

    #endregion

    #region Initialize

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("an instance of MainGameplay already exists");
        }

        Instance = this;

        StartCoroutine(DisplayAnnoncement(_textToDisplay, TextComponent, PanelForDisplay));

    }

    void Start()
    {
        _gameUIManager.RefreshTimer(_timerSeconds);

        _gameUIManager.Initialize(_player);
        _player.OnDeath += OnPlayerDeath;
        _player.OnLevelUp += OnLevelUp;
    }



    #endregion

    #region Update

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        _timerIncrement += Time.deltaTime;

        if (_timerIncrement >= 1)
        {
            _timerIncrement -= 1;
            _timerSeconds++;
            _gameUIManager.RefreshTimer(_timerSeconds);

            if (_timerSeconds >= _data.TimerToWin)
            {
                SetVictory();
            }
        }
    }

    #endregion

    #region Game Events

    internal void UnPause()
    {
        Time.timeScale = 1;
    }

    internal void Pause()
    {
        Time.timeScale = 0;
    }

    private void OnLevelUp(int level)
    {
        Pause();

        List<UpgradeData> upgrades = new List<UpgradeData>();
        upgrades.AddRange(_player.UpgradesAvailable);

        List<UpgradeData> randomUpgrades = new List<UpgradeData>();
        const int nbUpgrades = 3;
        for (int i = 0; i < nbUpgrades; i++)
        {
            if (upgrades.Count == 0)
                break;

            int rnd = Random.Range(0, upgrades.Count);
            UpgradeData upgrade = upgrades[rnd];
            upgrades.RemoveAt(rnd);
            randomUpgrades.Add(upgrade);
        }

        _gameUIManager.DisplayUpgrades(randomUpgrades.ToArray());
    }

    private void DisplayMessage(string message, TextMeshProUGUI text, GameObject panel)
    {
        Pause();
        text.text = message;
        panel.SetActive(true);
    }

    private void CloseMessage(GameObject panel)
    {
        panel.SetActive(false);
        UnPause();
    }

    public IEnumerator DisplayAnnoncement(string message, TextMeshProUGUI text, GameObject panel)
    {
        DisplayMessage(message, text, panel);
        yield return new WaitForSecondsRealtime(5f); 
        CloseMessage(panel);
    }

    void OnPlayerDeath()
    {
        State = GameState.GameOver;
        _gameUIManager.DisplayGameOver();
    }

    void SetVictory()
    {
        State = GameState.GameOver;
        _gameUIManager.DisplayVictory();
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region Tools

    public List<EnemyController> GetClosestEnemy(Vector3 position, int nbEnemy)
    {
        /*float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;*/

        Dictionary<EnemyController, float> distanceFromEnemies = new Dictionary<EnemyController, float>();

        foreach (var enemy in _enemies)
        {
            Vector3 direction = enemy.transform.position - position;

            float distance = direction.sqrMagnitude;

            distanceFromEnemies.Add(enemy, distance);
            /*if (distance < bestDistance)
            {
                bestDistance = distance;
                bestEnemy = enemy;
            }*/
        }
        var closestEnemies = distanceFromEnemies.
                OrderBy(key => key.Value).
                Take(nbEnemy).
                Select(key => key.Key).
                ToList();

        return closestEnemies;
    }

    List<EnemyController> _enemiesOnScreen = new List<EnemyController>();

    public EnemyController GetRandomEnemyOnScreen()
    {
        _enemiesOnScreen.Clear();

        foreach (var enemy in _enemies)
        {
            Vector3 viewport = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1)
                _enemiesOnScreen.Add(enemy);
        }

        if (_enemiesOnScreen.Count == 0)
            return null;

        int rnd = Random.Range(0, _enemiesOnScreen.Count);

        return _enemies[rnd];
    }

    #endregion
}