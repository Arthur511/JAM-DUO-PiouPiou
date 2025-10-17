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
    
    public int Slot { get; private set; }
    public GameObject GameObject {  get; private set; }
    
    protected float _timerCoolDown;
 
    public virtual void Initialize(WeaponData data)
    {
        _damageMax = data.Weapon._damageMax;
        _damageMax = data.Weapon._damageMin;
        _damageMax = data.Weapon._coolDown;
    }

    protected float GetDamage()
    {
        return Random.Range(_damageMin, _damageMax);
    }
    
    public abstract void Update(PlayerController player);
}
