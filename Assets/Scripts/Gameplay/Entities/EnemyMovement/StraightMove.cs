using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


[CreateAssetMenu(menuName = "EnemyMovements/Straight")]
public class StraightMove : EnemyMove
{
    public override Vector3 GetMovement(Transform enemy, Transform player, float speed)
    {
        Vector3 direction = player.transform.position - enemy.transform.position;
        direction.y = 0;

        float moveStep = speed* Time.deltaTime;
        if (moveStep >= direction.magnitude)
        {
            return Vector3.zero;
        }
        else
        {
            return direction.normalized;
        }
    }
}
