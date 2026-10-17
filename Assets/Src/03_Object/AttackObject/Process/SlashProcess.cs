using UnityEngine;

public class SlashProcess : MonoBehaviour
{
    private GameObject slashObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Quaternion followObjectRotation;
    private string trantitionTagName;
    private float currentPosiotionXValue = default;
    private float followObjectDistance = 1f; //追従対象との距離
    private float curentXPosition = default;
    private float attackPower = default;
    private int objectIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SlashProcess(Animator animator, GameObject slashObject)
    {
        this.animator = animator;
        this.slashObject = slashObject;
    }

    public void TargetObjectFollow()
    {
        if (currentPosiotionXValue < followObjectTransform.position.x)
        {
            curentXPosition =
                objectIndex == 0 ? followObjectTransform.position.x + followObjectDistance :
                objectIndex == 1 ? followObjectTransform.position.x - followObjectDistance :
                0;

            followObjectRotation =
                objectIndex == 0 ? Quaternion.Euler(0, 0, 0) :
                objectIndex == 1 ? Quaternion.Euler(0, 180, 0) :
                followObjectRotation;
        }
        else if (currentPosiotionXValue > followObjectTransform.position.x)
        {
            curentXPosition =
                objectIndex == 0 ? followObjectTransform.position.x - followObjectDistance :
                objectIndex == 1 ? followObjectTransform.position.x + followObjectDistance :
                0;
            followObjectRotation =
                objectIndex == 0 ? Quaternion.Euler(0, 180, 0) :
                objectIndex == 1 ? Quaternion.Euler(0, 0, 0) :
                followObjectRotation;
        }
        else
        {
            followObjectRotation = 
                objectIndex == 0 ? Quaternion.Euler(0, 0, 0) :
                objectIndex == 1 ? Quaternion.Euler(0, 180, 0) :
                followObjectRotation;

            curentXPosition = (followObjectRotation.y <= 0) ? followObjectTransform.position.x + followObjectDistance : followObjectTransform.position.x - followObjectDistance;
        }
        //対象を追従
        slashObject.gameObject.transform.position = new Vector3(
            curentXPosition,
            followObjectTransform.position.y,
            followObjectTransform.position.z);

        slashObject.transform.rotation = followObjectRotation;

        //最新のX座標を取得
        //追従しているオブジェクトのX座標を取得
        currentPosiotionXValue = followObjectTransform.transform.position.x;
    }

    public void Attack()
    {

        //アニメーションを開始
        animator.SetTrigger(trantitionTagName);
    }

    public bool IsAttackFinish;

    public void SetParameter(float power,string animationTriggerName)
    {
        this.attackPower = power;
        if(trantitionTagName != "") animator.ResetTrigger(trantitionTagName);
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
