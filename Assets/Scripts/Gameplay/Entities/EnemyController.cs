using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Represents an enemy who's moving toward the player
/// and damage him on collision
/// data bout the enemy are stored in the EnemyData class
/// CAUTION : don't forget to call Initialize when you create an enemy
/// </summary>
public class EnemyController : Unit
{
    GameObject _player;
    Rigidbody _rb;
    EnemyData _data;
    private List<PlayerController> _playersInTrigger = new List<PlayerController>();


    [Header("Enchantment")]
    public bool IsBewitch = false;
    private Vector3 _bewitchCenter;
    private float _bewitchRadius = 1f;
    private float _bewitchAngle = 0f;
    private float _bewitchSpeed = 135f;
    [HideInInspector]public float TimerEnchanted = 3f;
    [HideInInspector]public float CurrentTimerEnchanted;


    [SerializeField] ParticleSystem _hitParticule;

    [SerializeField]EnemyMove _movementType;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void Initialize(GameObject player, EnemyData data)
    {
        _player = player;
        _data = data;
        _life = data.Life;
        _team = 1;
    }

    private void Update()
    {
        if (_life <= 0)
            return;


        foreach (var player in _playersInTrigger)
        {
            player.Hit(Time.deltaTime * _data.DamagePerSeconds);
        }
    }

    void FixedUpdate()
    {
        if (!IsBewitch)
            MoveToPlayer();
        else
            Enchanted();
    }

    private void MoveToPlayer()
    {
        Vector3 velocity = _movementType.GetMovement(transform, _player.transform, _data.MoveSpeed);
        _rb.linearVelocity = velocity;

        /*Vector3 direction = _player.transform.position - transform.position;
        direction.y = 0;

        float moveStep = _data.MoveSpeed * Time.deltaTime;
        if (moveStep >= direction.magnitude)
        {
            _rb.linearVelocity = Vector3.zero;
            transform.position = _player.transform.position;
        }
        else
        {
            direction.Normalize();
            _rb.linearVelocity = direction * _data.MoveSpeed;
        }*/
    }

    private void Enchanted()
    {
        if (CurrentTimerEnchanted > 0)
        {
            CurrentTimerEnchanted-= Time.deltaTime;
            if (_bewitchCenter == Vector3.zero)
            {
                _bewitchCenter = transform.position;
                _bewitchAngle = 0f;
            }
            _bewitchAngle += _bewitchSpeed * Time.deltaTime;

            float rad = _bewitchAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * _bewitchRadius;
            Vector3 targetPos = _bewitchCenter + offset;

            Vector3 moveDir = (targetPos - transform.position).normalized;
            _rb.linearVelocity = moveDir * (_data.MoveSpeed*Random.Range(1.5f , 4));

        }
        else
        {
            IsBewitch = false;
        }

    }

    public override void Hit(float damage)
    {
        _life -= damage;
        ParticleManager.instance.PlayParticule(_hitParticule);
        if (Life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        MainGameplay.Instance.Enemies.Remove(this);
        GameObject.Destroy(gameObject);
        MainGameplay.Instance.Player.IsHit = false;
        var xp = GameObject.Instantiate(MainGameplay.Instance.PrefabXP, transform.position, Quaternion.identity);
        xp.GetComponent<CollectableXp>().Initialize(1);

    }
    public void DieFromFlute()
    {
        MainGameplay.Instance.Enemies.Remove(this);
        MainGameplay.Instance.Player.IsHit = false;
        GameObject.Destroy(gameObject);
        var xp = GameObject.Instantiate(MainGameplay.Instance.SuperPrefabXP, transform.position, Quaternion.identity);
        xp.GetComponent<CollectableXp>().Initialize(5);
    }


    private void OnTriggerEnter(Collider col)
    {
        var other = col.GetComponentInParent<PlayerController>();

        if (other != null)
        {
            if (_playersInTrigger.Contains(other) == false)
            {
                _playersInTrigger.Add(other);
                other.IsHit = true;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        var other = col.GetComponentInParent<PlayerController>();

        if (other != null)
        {
            if (_playersInTrigger.Contains(other))
            {
                _playersInTrigger.Remove(other);
                other.IsHit = false;
            }
        }
    }
}