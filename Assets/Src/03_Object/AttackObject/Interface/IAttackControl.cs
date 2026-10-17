using UnityEngine;

public interface IAttackControl
{
    void IsAddObject(GameObject[] attackObject, GameObject followObject);

    void UpLeveling();

    int GetCurrentLevel();
}
