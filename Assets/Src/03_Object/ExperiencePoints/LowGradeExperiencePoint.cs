using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LowGradeExperiencePoint : ExperiencePointBase, IExperiencePoint
{
    public override int experiencePoints => 1;

    public Vector2Int cell;
    protected override string objectName => ExperiencePointsObjectPoolName.LowGradePoints.ToString();

    private GameObject targetObject;
    public bool haspermission = default;
    private float moveSpeed = 7f;

    void Start()
    {
        
    }

    void OnEnable()
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
        }
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

                haspermission = false;

                Debug.Log($"haspermission ：{haspermission}");
                EventManager.Instance.PushExperiencePointEvent(objectName, this.gameObject);
            }
        }
    }
}
