using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents the in-game lifebar of the player
/// </summary>
public class LifeBar : MonoBehaviour
{
    [SerializeField] Image _spriteImage;
    //[SerializeField] Transform _transform;

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        _spriteImage.fillAmount = value;
    }

    public void SetValue(float current , float maxValue)
    {
        float value = current / maxValue;
        value = Mathf.Clamp01(value);
        _spriteImage.fillAmount = value;
    }

    /*void LateUpdate()
    {
        transform.position = _transform.position;
        transform.rotation = _transform.rotation;  
    }*/

}
