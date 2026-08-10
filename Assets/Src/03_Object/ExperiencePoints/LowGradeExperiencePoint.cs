using UnityEngine;

public class LowGradeExperiencePoint : ExperiencePointBase, IExperiencePoint
{
    public override int experiencePoints => 1;

    private Vector2Int cell;
    protected override string objectName => ExperiencePointsObjectPoolName.LowGradePoints.ToString();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if(collision.gameObject.tag == AcquisitionObjectName.Player.ToString() && collision.TryGetComponent<IPlayer>(out var player))
        {
            player.SetExperiencePoint(experiencePoints);

            EventManager.Instance.PushExperiencePointEvent(objectName, this.gameObject);
        }
    }
}
