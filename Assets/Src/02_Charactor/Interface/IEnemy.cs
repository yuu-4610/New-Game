using UnityEngine;

public interface IEnemy
{
    void TakeDamage(float damage);
    void SetTargetObject(GameObject targetObject);

    void SetCurrentCell(Vector2Int currentCell);
    Vector2Int GetCurrentCell();
}
