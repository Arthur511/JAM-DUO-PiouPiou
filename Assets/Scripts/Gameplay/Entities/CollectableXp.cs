using UnityEngine;

/// <summary>
/// Represents the xp points the player has to collect to level up
/// </summary>
public class CollectableXp : MonoBehaviour
{
    public int Value { get; private set; }
    [SerializeField] float _speed;
    public void Initialize(int value)
    {
        Value = value;
    }
    
    void OnTriggerEnter(Collider col)
    {
        var other = col.GetComponent<PlayerController>();
        
        if (other != null)
        {
            other.CollectXP(Value);
            GameObject.Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, MainGameplay.Instance.Player.transform.position) < 3)
        {
            transform.position = Vector3.Lerp(transform.position, MainGameplay.Instance.Player.transform.position, _speed* Time.deltaTime);
        }
    }

}
