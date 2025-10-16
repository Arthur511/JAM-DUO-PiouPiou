using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class IntertitreStroboscopeScript : MonoBehaviour
{
    [SerializeField] Image _image;
    private Color _color;

    private void Awake()
    {
        _color = _image.color;
    }

    void FixedUpdate()
    {
        _color.a = Random.Range(0.0f, 0.02f);
        _image.color = _color;
    }
}