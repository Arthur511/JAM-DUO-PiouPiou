using UnityEditor.Overlays;
using UnityEngine;

/// <summary>
/// Instance of a wave
/// keeps the wave alive while there's still enemies to spawn
/// </summary>
public class WaveInstance
{
    public bool IsDone => _currentOccurence >= _waveData.TimesToRepeat;
    
    public WaveData WaveData => _waveData;

    WaveData _waveData;
    float _timer;
    int _currentOccurence;

    public WaveInstance(WaveData data)
    {
        _waveData = data;
    }

    public void Update( WavesManager manager, float currentTimer)
    {
        if (_waveData.TimeToStart > currentTimer)     
            return;

        /*if (currentTimer == _waveData.TimeToStart && _waveData.CanDisplayMessage == true)
        {
            MainGameplay.Instance.
            StartCoroutine(MainGameplay.Instance.DisplayAnnoncement(manager._messageList[manager._messageIndex],
            MainGameplay.Instance.TextComponent,
            MainGameplay.Instance.PanelForDisplay));
            manager._messageIndex++;
        }*/

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _currentOccurence++;
            _timer = _waveData.RepeatTimer;

            manager.Spawn(_waveData);
        }

    }
}
