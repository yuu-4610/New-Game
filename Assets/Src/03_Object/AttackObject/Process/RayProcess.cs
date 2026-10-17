using UnityEngine;

public class RayProcess
{
    private GameObject rayObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Quaternion followObjectRotation;
    private bool isStartAnime = default;
    private bool isCollision = default;
    private string trantitionTagName;
    private string attackAnimationName;
    private float currentPosiotionXValue = default;
    private float followObjectDistance = 2f; //追従対象との距離
    private Vector3 currentPosition = default;
    private float attackPower = default;
    private bool isCordinateLock = default;
    private int objectIndex;


    public RayProcess(Animator animator, GameObject rayObject)
    {
        this.animator = animator;
        this.rayObject = rayObject;

    }

    public void TargetObjectFollow()
    {
        if (currentPosiotionXValue < followObjectTransform.position.x)
        {
            currentPosition =
                objectIndex == 0 ? new Vector3(followObjectTransform.position.x + followObjectDistance, followObjectTransform.position.y, followObjectTransform.position.z) :
                objectIndex == 1 ? new Vector3(followObjectTransform.position.x - followObjectDistance, followObjectTransform.position.y, followObjectTransform.position.z) :
                objectIndex == 2 ? new Vector3(followObjectTransform.position.x, followObjectTransform.position.y + followObjectDistance, followObjectTransform.position.z) :
                objectIndex == 3 ? new Vector3(followObjectTransform.position.x, followObjectTransform.position.y - followObjectDistance, followObjectTransform.position.z) :
                Vector3.zero;

            followObjectRotation =
                objectIndex == 0 ? Quaternion.Euler(0, 0, 0) :
                objectIndex == 1 ? Quaternion.Euler(0, 180, 0) :
                objectIndex == 2 ? Quaternion.Euler(0, 0, 90) :
                objectIndex == 3 ? Quaternion.Euler(0, 0, -90) :
                followObjectRotation;
        }
        else if (currentPosiotionXValue > followObjectTransform.position.x)
        {
            currentPosition =
                objectIndex == 0 ? new Vector3(followObjectTransform.position.x - followObjectDistance, followObjectTransform.position.y, followObjectTransform.position.z) :
                objectIndex == 1 ? new Vector3(followObjectTransform.position.x + followObjectDistance, followObjectTransform.position.y, followObjectTransform.position.z) :
                objectIndex == 2 ? new Vector3(followObjectTransform.position.x, followObjectTransform.position.y + followObjectDistance, followObjectTransform.position.z) :
                objectIndex == 3 ? new Vector3(followObjectTransform.position.x, followObjectTransform.position.y - followObjectDistance, followObjectTransform.position.z) :
                Vector3.zero;
            followObjectRotation =
                objectIndex == 0 ? Quaternion.Euler(0, 180, 0) :
                objectIndex == 1 ? Quaternion.Euler(0, 0, 0) :
                objectIndex == 2 ? Quaternion.Euler(0, 0, 90) :
                objectIndex == 3 ? Quaternion.Euler(0, 0, -90) :
                followObjectRotation;
        }
        else
        {
            followObjectRotation =
                objectIndex == 0 ? Quaternion.Euler(0, 0, 0) :
                objectIndex == 1 ? Quaternion.Euler(0, 180, 0) :
                objectIndex == 2 ? Quaternion.Euler(0, 0, 90) :
                objectIndex == 3 ? Quaternion.Euler(0, 0, -90) :
                followObjectRotation;

            currentPosition =
                //objectIndex = 2,3の場合
                (followObjectRotation.z != 0) ? new Vector3(followObjectRotation.x, (followObjectRotation.z > 0) ? followObjectTransform.position.y + followObjectDistance : -followObjectTransform.position.y - followObjectDistance, followObjectTransform.position.z) :
                //objectIndex = 0の場合
                (followObjectRotation.y <= 0) ? new Vector3(followObjectTransform.position.x + followObjectDistance, followObjectTransform.position.y, followObjectTransform.position.z) :
                //objectIndex = 1の場合
                (followObjectRotation.y > 0) ? new Vector3(followObjectTransform.position.x - followObjectDistance, followObjectTransform.position.y, followObjectTransform.position.z) :
                currentPosition;
        }
        Debug.Log($"currentXPosition：{currentPosition}");
        //対象を追従
        rayObject.gameObject.transform.position = currentPosition;

        rayObject.transform.rotation = followObjectRotation;

        //最新のX座標を取得
        //追従しているオブジェクトのX座標を取得
        currentPosiotionXValue = followObjectTransform.transform.position.x;
    }

    public void Attack()
    {
        //アニメーションを開始
        animator.SetTrigger(trantitionTagName);
    }

    public void SetParameter(float power, string animationTriggerName)
    {
        this.attackPower = power;
        if (trantitionTagName != "") animator.ResetTrigger(trantitionTagName);
        SetTagName(animationTriggerName);
    }

    public void InitializeProcess(GameObject playerObject, int objectIndex)
    {
        followObjectTransform = playerObject.transform;
        this.objectIndex = objectIndex;
    }

    private void SetTagName(string trantitionTagName)
    {
        this.trantitionTagName = trantitionTagName;
    }

    public void CollisionEnemy(Collider2D collision)
    {
        if (collision.TryGetComponent<IEnemy>(out var damageable))
        {
            damageable.TakeDamage(attackPower);
        }
    }
}