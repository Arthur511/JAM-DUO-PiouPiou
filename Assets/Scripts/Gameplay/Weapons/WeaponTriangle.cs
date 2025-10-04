using Unity.VisualScripting;
using UnityEngine;
namespace Gameplay.Weapons
{
    public class WeaponTriangle : WeaponBase
    {
        
        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;
            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            Collider[] hits = Physics.OverlapSphere(player.transform.position, 4);
            
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
                {
                    enemy.IsEnchant = true;
                    enemy.CurrentTimerEnchanted = enemy.TimerEnchanted;
                }
            }


        }

    }
}
