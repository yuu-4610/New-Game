using UnityEngine;

public class MiddleGradeExperiencePoint : ExperiencePointBase, IExperiencePoint
{
    public override int experiencePoints => 5;

    protected override string objectName => ExperiencePointsObjectPoolName.MiddleGradePoints.ToString();

    private Vector2Int cell;

    void Start()
    {

    }
    public void SetCell(Vector2Int cell)
    {
        this.cell = cell;
    }

    public Vector2Int GetCell()
    {
        return cell;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerÇ…è’ìÀÇµÇΩÇÁÅAåoå±ílÇìnÇ∑
        if (collision.gameObject.tag == AcquisitionObjectName.Player.ToString() && collision.TryGetComponent<IPlayer>(out var player))
        {
            player.SetExperiencePoint(experiencePoints);

            EventManager.Instance.PushExperiencePointEvent(objectName, this.gameObject);
        }
    }
}
