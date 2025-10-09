using UnityEngine;

public class StroboscopicEffect : MonoBehaviour
{

    [SerializeField] MeshRenderer _meshRenderer;

    Color _materialColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _materialColor = _meshRenderer.material.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _materialColor.a = Random.Range(0.0f, 0.03f);

        _meshRenderer.material.color = _materialColor;
    }
}
