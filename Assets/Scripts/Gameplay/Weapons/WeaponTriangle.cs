using Unity.VisualScripting;
using UnityEngine;
namespace Gameplay.Weapons
{
    public class WeaponTriangle : WeaponBase
    {

        [SerializeField] AudioClip _triangleSound;
        [SerializeField] AudioSource _triangleAudioSource;
        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;
            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            Collider[] hits = Physics.OverlapSphere(player.transform.position, 4);
            
            AudioManager.instance.PlayASound(_triangleAudioSource, _triangleSound);

            foreach (Collider hit in hits)
            {
                if (hit.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
                {
                    enemy.IsBewitch = true;
                    enemy.CurrentTimerEnchanted = enemy.TimerEnchanted;
                }
            }

        }

    }
}
