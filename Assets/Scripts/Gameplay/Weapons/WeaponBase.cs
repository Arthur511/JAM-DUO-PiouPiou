using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the base class of all the player's weapons
/// </summary>
public abstract class WeaponBase
{
    [SerializeField] protected float _damageMin;
    [SerializeField] protected float _damageMax;
    [SerializeField] protected float _coolDown;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _range;
    [SerializeField] protected GameObject _weaponPrefab;
    [SerializeField] protected int _maxProjectileInShoot;

    public int Slot { get; private set; }

    protected float _timerCoolDown;

    public void Initialize(int slot)
    {
        Slot = slot;
    }

    protected float GetDamage()
    {
        return Random.Range(_damageMin, _damageMax);
    }

    public abstract void Update(PlayerController player);
}
