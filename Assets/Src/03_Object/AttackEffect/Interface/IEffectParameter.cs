using UnityEngine;

public interface IEffectParameter
{
    void InitialSetting(float coolTime, GameObject followObject);

    void SetObjectPosition(float followObjectDistance, Vector2 objectScale);

    void SetAttackPermission(bool permission);
}
