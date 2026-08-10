using UnityEngine;

public abstract class ExperiencePointBase : MonoBehaviour
{
    public abstract int experiencePoints { get; }

    protected abstract string objectName { get; }
}
