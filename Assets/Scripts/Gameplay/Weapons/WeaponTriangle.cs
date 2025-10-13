using Unity.VisualScripting;
using UnityEngine;
namespace Gameplay.Weapons
{
    public class WeaponTriangle : WeaponBase
    {
        [SerializeField] GameObject _prefab;
        [Range(1, 10)][SerializeField] float _radius;

        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;
            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            Collider[] hits = Physics.OverlapSphere(player.transform.position, _radius);
            
            AudioManager.instance.PlayASound(AudioManager.instance.TriangleAudioSource);
            GameObject go = GameObject.Instantiate(_prefab, player.transform.position, Quaternion.LookRotation(Vector3.up));
            var radius = go.GetComponent<ParticleSystem>().shape.radius;
            radius = _radius;
            GameObject.Destroy(go.gameObject, 0.8f);
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
