using UnityEngine;

public interface IAttackObject
{
    void InitialSetting(float coolTime, GameObject followObject);

    void SetObjectPosition(float followObjectDistance, Vector2 objectScale);

    void SetAttackPermission(bool permission);

    bool IsUpLevelingPossible();

    void UpLeveling();
}
