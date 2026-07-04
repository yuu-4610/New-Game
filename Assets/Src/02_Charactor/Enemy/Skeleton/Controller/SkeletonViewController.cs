using UnityEngine;

public class SkeletonViewController : MonoBehaviour
{
    private Animator animator;
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
        animator.SetBool(SkeletonAnimationTriggerName.MoveBool.ToString(), false);
        Debug.Log("Idleアニメーション");
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
        animator.SetBool(SkeletonAnimationTriggerName.MoveBool.ToString(), true);
        Debug.Log("Moveアニメーション");
    }
}
