using JetBrains.Annotations;
using UnityEngine;

public abstract class EnemyMove : ScriptableObject, IEnemyMovement
{
    public abstract Vector3 GetMovement(Transform enemy, Transform player, float speed);
}
