using UnityEngine;

public class SkeletonViewController : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo animtorStateInfo;
    private bool isRigthDirectionFacing, isLeftDirectionFacing = default;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    public void SkeletonIdleAnimation()
    {
        animator.SetBool(EnemyAnimationTriggerName.MoveBool.ToString(), false);
        animator.SetBool(EnemyAnimationTriggerName.DieBool.ToString(), false);
    }

    public void SkeletonMoveAnimation(float directionFacing)
    {
        if (directionFacing >= 0.1f && !isRigthDirectionFacing)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            isRigthDirectionFacing = true;
            isLeftDirectionFacing = false;

            //Debug.Log($"右向き R：{isRigthDirectionFacing} L：{isLeftDirectionFacing} Rotate：{transform.rotation}");
        }
        else if (directionFacing <= -0.1f && !isLeftDirectionFacing)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            isLeftDirectionFacing = true;
            isRigthDirectionFacing = false;

            //Debug.Log($"左向き R：{isRigthDirectionFacing} L：{isLeftDirectionFacing} Rotate：{transform.rotation}");
        }
        animator.SetBool(EnemyAnimationTriggerName.MoveBool.ToString(), true);
    }
    public void SkeletonDieAnimation()
    {
        animator.SetBool(EnemyAnimationTriggerName.DieBool.ToString(), true);
    }

    public AnimatorStateInfo GetStateInfo()
    {
        animtorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return animtorStateInfo;
    }
}
