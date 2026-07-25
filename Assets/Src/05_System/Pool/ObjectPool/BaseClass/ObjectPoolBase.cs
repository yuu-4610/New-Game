using UnityEngine;

public abstract class ObjectPoolBase : MonoBehaviour
{
    public abstract GameObject Pop(string objectName);

    public abstract void Push(string returnObjectName, GameObject returnObject);
}
