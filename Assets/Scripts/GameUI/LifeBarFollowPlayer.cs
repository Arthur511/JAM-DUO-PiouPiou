using UnityEngine;

public class LifeBarFollowPlayer : MonoBehaviour
{

    [SerializeField] GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + new Vector3(-.3f, 0, 1);
    }
}
