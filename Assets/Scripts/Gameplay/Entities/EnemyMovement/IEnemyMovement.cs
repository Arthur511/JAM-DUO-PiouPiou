using UnityEngine;

public interface IEnemyMovement
{
    Vector3 GetMovement(Transform enemy, Transform player, float speed);
}
