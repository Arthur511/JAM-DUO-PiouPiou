using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    /// <summary>
    /// Represents a weapon that shot one or many bullets at a time to the closest enemies
    /// </summary>
    public class WeaponBullet : WeaponBase
    {
        public WeaponBullet()
        {
        }

        /*public override void Initialize(int Slot)
        {
            base.Initialize(Slot);

            _prefab = bulletData.Prefab;
            _speed = bulletData.Speed;
            _maxProjectileInShoot = bulletData.MaxProjectileInShoot;
            
        }*/


        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;

            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            List<EnemyController> enemies = MainGameplay.Instance.GetClosestEnemy(player.transform.position, _maxProjectileInShoot);
            if (enemies == null)
                return;

            player.StartCoroutine(ShootEnemies(enemies, player));
            
        }
        private IEnumerator ShootEnemies(List<EnemyController> enemies, PlayerController player)
        {
            if (enemies.Count > 0)
            {
                foreach (EnemyController enemy in enemies)
                {

                    if (enemy == null) continue;

                    var playerPosition = player.transform.position + Vector3.up;
                    if (enemy.transform == null) continue;

                    Vector3 direction = enemy.transform.position - playerPosition;
                    if (direction.sqrMagnitude > 0)
                    {
                        direction.Normalize();
                    }
                    GameObject go = GameObject.Instantiate(_weaponPrefab, playerPosition, Quaternion.identity);
                    go.GetComponent<Bullet>().Initialize(direction, GetDamage(), _speed);

                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}