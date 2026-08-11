using UnityEngine;

public class LowGradeExperiencePoint : ExperiencePointBase, IExperiencePoint
{
    public override int experiencePoints => 1;

    private Vector2Int cell;
    protected override string objectName => ExperiencePointsObjectPoolName.LowGradePoints.ToString();

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
        //Playerに衝突したら、経験値を渡す
        if(collision.gameObject.tag == AcquisitionObjectName.Player.ToString())
        {
            //Debug.Log("プレイヤーが触れた");
            if(collision.TryGetComponent<IPlayer>(out var player))
            {
                player.SetExperiencePoint(experiencePoints);

                //EventManager.Instance.PushExperiencePointEvent(objectName, this.gameObject);
            }
        }
    }
}
