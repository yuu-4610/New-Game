using UnityEngine;

public class SpearSkeletonViewController : MonoBehaviour
{
    private Animator animator;
    private bool isRigthDirectionFacing, isLeftDirectionFacing = default;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SpearSkeletonAnimationIdle()
    {
        animator.SetBool(SlimeAnimationTriggerName.MoveBool.ToString(), false);
    }

    public void SpearSkeletonAnimationMove(float directionFacing)
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
    }

    public void SpearSkeletonAnimationDie()
    {
        animator.SetBool(SlimeAnimationTriggerName.MoveBool.ToString(), true);
    }
}