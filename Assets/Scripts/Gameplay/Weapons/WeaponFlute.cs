using UnityEngine;
namespace Gameplay.Weapons
{
    public class WeaponFlute : WeaponBase
    {
        [SerializeField] GameObject _prefab;
        [Range(10, 20)][SerializeField] float _maxDistance;

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

            RaycastHit[] hits = Physics.RaycastAll(player.transform.position, (player.transform.forward).normalized, _maxDistance);
            //Debug.DrawRay(player.transform.position, (player.transform.forward).normalized * _maxDistance, Color.red, 0.5f);

            Quaternion direction = Quaternion.LookRotation(player.transform.forward, Vector3.forward);
            //Quaternion orientation = Quaternion.Euler(90f, 0f, 0f);
            go = GameObject.Instantiate(_prefab, player.transform.position + direction*player.transform.forward, direction);
            var radius = go.GetComponent<ParticleSystem>().main;
            radius.startLifetime = _maxDistance * 0.05f;
            go.GetComponent<ParticleSystem>().Play();
            GameObject.Destroy(go.gameObject, 0.8f);
            AudioManager.instance.PlayASound(AudioManager.instance.FluteAudioSource);

            go.transform.localScale = new Vector3(go.transform.localScale.x/2, go.transform.localScale.y, go.transform.localScale.z * _maxDistance);
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

