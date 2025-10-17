using UnityEngine;

public class WeaponTimpaniData : WeaponData
{
    [SerializeField] GameObject _prefab;
    [Range(1, 10)][SerializeField] float _radius;

    public float Radius => _radius;
    public GameObject Prefab => _prefab;
}
