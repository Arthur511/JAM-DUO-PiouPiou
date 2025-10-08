using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.Weapons
{

    public class WeaponTimpani : WeaponBase
    {
        [SerializeField] GameObject _prefab;
        [Range(1, 10)][SerializeField] float _radius;

        [SerializeField] AudioSource _timpaniAudioSource;
        [SerializeField] AudioClip _timpaniSound;

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
            go = GameObject.Instantiate(_prefab, player.transform.position, Quaternion.LookRotation(Vector3.up), player.transform);

            AudioManager.instance.PlayASound(_timpaniAudioSource, _timpaniSound);

            float diameter = _radius * 2f;
            Vector3 spriteSize = go.GetComponent<SpriteRenderer>().sprite.bounds.size;
            go.transform.localScale = new Vector3(diameter/spriteSize.x , diameter / spriteSize.y, 1f);
            GameObject.Destroy(go.gameObject, 0.3f);
            foreach (Collider hit in hits)
            {
                if(hit.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
                {
                    enemy.Hit(Random.Range(_damageMin, _damageMax));
                }
            }
        }
    }
}
