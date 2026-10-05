using UnityEngine;

public interface IExperiencePoint
{
    void SetCell(Vector2Int cell);

    Vector2Int GetCell();

    void PushProcessCheck();

    void SetTargetObject(GameObject playerObject);
}
