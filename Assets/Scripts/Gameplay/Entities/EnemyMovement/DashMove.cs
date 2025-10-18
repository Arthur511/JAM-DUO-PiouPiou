using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;


[CreateAssetMenu(menuName = "EnemyMovements/Dash")]
public class DashMove : EnemyMove
{
    
    float _currentTimer;
    public override Vector3 GetMovement(Transform enemy, Transform player, float speed)
    {
        _currentTimer += Time.deltaTime/2;

        if ((int)_currentTimer % 2 == 0)
        {
            Vector3 direction = player.transform.position - enemy.transform.position;
            direction.y = 0;

            float moveStep = speed * Time.deltaTime;
            return direction.normalized;
        }
        return Vector3.zero;
    }
}
