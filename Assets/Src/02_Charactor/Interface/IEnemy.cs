using UnityEngine;

public interface IEnemy
{
    void TakeDamage(float damage);
    void SetTargetObject(GameObject targetObject);
}
