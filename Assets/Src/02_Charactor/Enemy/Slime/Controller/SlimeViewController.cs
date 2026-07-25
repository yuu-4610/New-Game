using UnityEngine;

public class SlimeViewController : MonoBehaviour
{
    private Animator animator;
    private bool isRigthDirectionFacing, isLeftDirectionFacing = default;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SlimeAnimationIdle()
    {
        animator.SetBool(EnemyAnimationTriggerName.MoveBool.ToString(), false);
    }

    public void SlimeAnimationMove(float directionFacing)
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

    public void SlimeAnimationDie()
    {

    }
}
