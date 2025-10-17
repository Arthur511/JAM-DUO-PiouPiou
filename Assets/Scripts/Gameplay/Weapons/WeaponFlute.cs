using UnityEngine;
namespace Gameplay.Weapons
{
    public class WeaponFlute : WeaponBase
    {

        GameObject go;

        public WeaponFlute()
        {
            
        }
        public override void Update(PlayerController player)
        {
            _timerCoolDown += Time.deltaTime;
            if (_timerCoolDown < _coolDown)
                return;

            _timerCoolDown -= _coolDown;

            RaycastHit[] hits = Physics.RaycastAll(player.transform.position, (player.transform.forward).normalized, _range);

            Quaternion direction = Quaternion.LookRotation(player.transform.forward, Vector3.forward);
            go = GameObject.Instantiate(_weaponPrefab, player.transform.position + direction*player.transform.forward, direction);
            var radius = go.GetComponent<ParticleSystem>().main;
            radius.startLifetime = _range * 0.05f;
            go.GetComponent<ParticleSystem>().Play();
            GameObject.Destroy(go.gameObject, 0.8f);
            AudioManager.instance.PlayASound(AudioManager.instance.FluteAudioSource);

            go.transform.localScale = new Vector3(go.transform.localScale.x/2, go.transform.localScale.y, go.transform.localScale.z * _range);
            GameObject.Destroy(go, 0.3f);
            if (hits.Length > 0)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.TryGetComponent<EnemyController>(out EnemyController enemy))
                    {
                        enemy.DieFromFlute();
                    }
                }
            }
        }
    }

}

