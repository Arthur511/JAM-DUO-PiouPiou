using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

/// <summary>
/// Manages the waves : stores the data, updates and plays them 
/// </summary>
public class WavesManager : MonoBehaviour
{
    [SerializeField] WavesLevelData _wavesLevel;
    [SerializeField] Transform _topRight;
    [SerializeField] Transform _bottomLeft;

    readonly List<WaveInstance> _wavesToPlay = new List<WaveInstance>();
    float _timer;


    [SerializeField] List<string> _messageList = new List<string>();
    int _messageIndex = 0;

    void Awake()
    {

        foreach (var data in _wavesLevel.Waves)
        {
            WaveInstance instance = new WaveInstance(data);
            _wavesToPlay.Add(instance);
        }
    }

    void Update()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
            return;

        _timer += Time.deltaTime;

        for (int i = _wavesToPlay.Count - 1; i >= 0; i--)
        {
            _wavesToPlay[i].Update(this, _timer);

            if (_wavesToPlay[i].IsDone)
            {
                if (_wavesToPlay[i].WaveData.CanDisplayMessage == true)
                {
                    MainGameplay.Instance.
                    StartCoroutine(MainGameplay.Instance.DisplayAnnoncement(_messageList[_messageIndex],
                    MainGameplay.Instance.TextComponent,
                    MainGameplay.Instance.PanelForDisplay));
                    _messageIndex++;
                }
                _wavesToPlay.RemoveAt(i);
            }
        }
    }

    public void Spawn(WaveData data)
    {
        for (int i = 0; i < data.EnemyCount; i++)
        {
            Vector2 randomC = Random.insideUnitCircle.normalized;
            Vector3 direction = new Vector3(randomC.x, 0, randomC.y);

            Vector3 spawnPosition = MainGameplay.Instance.Player.transform.position + direction * data.SpawnDistance;
            spawnPosition.x = Mathf.Clamp(spawnPosition.x, _bottomLeft.position.x, _topRight.position.x);
            spawnPosition.z = Mathf.Clamp(spawnPosition.z, _bottomLeft.position.z, _topRight.position.z);

            GameObject go = GameObject.Instantiate(data.Enemy.Prefab, spawnPosition, Quaternion.identity);

            var enemy = go.GetComponent<EnemyController>();
            enemy.Initialize(MainGameplay.Instance.Player.gameObject, data.Enemy);
            MainGameplay.Instance.Enemies.Add(enemy);
        }
    }
}