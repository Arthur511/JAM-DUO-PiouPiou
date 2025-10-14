using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Represents the in-game lifebar of the player
/// </summary>
public class LifeBar : MonoBehaviour
{
    [SerializeField] Image _spriteImage;
    [SerializeField] Image _iconImage;
    int comptHit = 0;
    //[SerializeField] Transform _transform;

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
            comptHit += (int)Time.deltaTime;
            if (comptHit % 2 == 0)
            {
                _iconImage.color = Color.black;
            }
            else if (comptHit % 2 != 0)
            {
                _iconImage.color = Color.white;
            }
        }
        else
        {
            _iconImage.color = Color.white;
        }
    }
}
