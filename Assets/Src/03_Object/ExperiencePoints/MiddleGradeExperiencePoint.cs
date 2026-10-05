using UnityEngine;

public class MiddleGradeExperiencePoint : ExperiencePointBase, IExperiencePoint
{
    public override int experiencePoints => 5;

    protected override string objectName => ExperiencePointsObjectPoolName.MiddleGradePoints.ToString();

    private Vector2Int cell;

    private GameObject targetObject;
    private bool haspermission = default;
    private float moveSpeed = 7f;

    void Start()
    {

    }

    void Update()
    {
        MoveToPlayer();
    }

    public void SetCell(Vector2Int cell)
    {
        this.cell = cell;
    }

    public Vector2Int GetCell()
    {
        return cell;
    }
    public void PushProcessCheck()
    {
        haspermission = true;
    }

    public void SetTargetObject(GameObject targetPObject)
    {
        this.targetObject = targetPObject;
    }
    private void MoveToPlayer()
    {
        if (haspermission)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, moveSpeed * Time.deltaTime);
            //transform.position += targetObject.transform.position - this.transform.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PlayerÇ…è’ìÀÇµÇΩÇÁÅAåoå±ílÇìnÇ∑
        if (collision.gameObject.tag == AcquisitionObjectName.Player.ToString() && collision.TryGetComponent<IPlayer>(out var player))
        {
            player.SetExperiencePoint(experiencePoints);

            haspermission = false;
            EventManager.Instance.PushExperiencePointEvent(objectName, this.gameObject);
        }
    }
}
