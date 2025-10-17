using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.Weapons
{

    public class WeaponTimpani : WeaponBase
    {

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

            Collider[] hits = Physics.OverlapSphere(player.transform.position, _range);
            go = GameObject.Instantiate(_weaponPrefab, player.transform.position, Quaternion.LookRotation(Vector3.up));

            AudioManager.instance.PlayASound(AudioManager.instance.TimpaniAudioSource);

            var radius = go.GetComponent<ParticleSystem>().main;
            radius.startLifetime = _range*0.1f;
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
