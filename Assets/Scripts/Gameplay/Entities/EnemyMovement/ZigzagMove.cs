using UnityEngine;


[CreateAssetMenu(menuName = "EnemyMovements/Zigzag")]
public class ZigzagMove : EnemyMove
{
    public float _frequence = 3;
    public float _amplitude = 2;
    public override Vector3 GetMovement(Transform enemy, Transform player, float speed)
    {
        Vector3 direction = player.transform.position - enemy.transform.position;
        direction.y = 0;

        Vector3 otherDirection = new Vector3(-direction.z, 0, direction.x);
        float sinusValue = Mathf.Sin(Time.time * _frequence) * _amplitude;

        return (direction+otherDirection*sinusValue).normalized * speed;
    }
}
