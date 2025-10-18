using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

/// <summary>
/// Represents the in-game lifebar of the player
/// </summary>
public class LifeBar : MonoBehaviour
{
    [SerializeField] Image _spriteImage;
    [SerializeField] Image _iconImage;

    Coroutine _currentCoroutine;

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        _spriteImage.fillAmount = value;
    }

    public void SetValue(float current, float maxValue)
    {
        float value = current / maxValue;
        value = Mathf.Clamp01(value);
        _spriteImage.fillAmount = value;
    }

    private void Update()
    {
        if (MainGameplay.Instance.Player.IsHit)
        {
            if (_currentCoroutine == null)
            {
                _currentCoroutine = StartCoroutine(BlinkPlayerIcon());
            }
        }
        else
        {
            _iconImage.color = Color.white;
        }
    }

    IEnumerator BlinkPlayerIcon()
    {
        _iconImage.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        _iconImage.color = Color.white;
        yield return new WaitForSeconds(0.1f);

        _currentCoroutine = null;
    }

}
