using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.Weapons
{

    public class WeaponTimpani : WeaponBase
    {
        [SerializeField] GameObject _prefab;
        [Range(1, 10)][SerializeField] float _radius;

        GameObject go;

        public WeaponTimpani()
        {
            
        }

        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;
            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            Collider[] hits = Physics.OverlapSphere(player.transform.position, _radius);
            go = GameObject.Instantiate(_prefab, player.transform.position, Quaternion.LookRotation(Vector3.up));

            AudioManager.instance.PlayASound(AudioManager.instance.TimpaniAudioSource);

            var radius = go.GetComponent<ParticleSystem>().main;
            radius.startLifetime = _radius*0.1f;
            go.GetComponent<ParticleSystem>().Play();
            GameObject.Destroy(go.gameObject, 0.8f);
            foreach (Collider hit in hits)
            {
                if(hit.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
                {
                    enemy.gameObject.GetComponent<Rigidbody>().AddForce((enemy.transform.position-player.transform.position) *30f, ForceMode.Impulse);
                    enemy.Hit(Random.Range(_damageMin, _damageMax));
                }
            }
        }
    }
}
