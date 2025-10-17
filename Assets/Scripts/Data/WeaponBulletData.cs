using UnityEngine;

public class WeaponBulletData : WeaponData
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _speed;
    [SerializeField] int _maxProjectileInShoot;

    public GameObject Prefab => _prefab;
    public float Speed => _speed;
    public int MaxProjectileInShoot => _maxProjectileInShoot;
}
