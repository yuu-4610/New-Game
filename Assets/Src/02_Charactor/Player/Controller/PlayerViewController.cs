using UnityEngine;

public class PlayerViewController : MonoBehaviour
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

    public void PlayerIdleAnimation()
    {
        animator.SetBool(PlayerAnimationTriggerName.MoveBool.ToString(), false);

        //Debug.Log("Idleアニメーション");
    }

    public void PlayerMoveAnimation(float virticalMoveValue)
    {
        //Debug.Log($"virticalMoveValue：{virticalMoveValue}");

        if (virticalMoveValue >= 1f && !isRigthDirectionFacing)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            isRigthDirectionFacing = true;
            isLeftDirectionFacing = false;

            //Debug.Log($"右向き R：{isRigthDirectionFacing} L：{isLeftDirectionFacing} Rotate：{transform.rotation}");
        }
        else if (virticalMoveValue <= -1f && !isLeftDirectionFacing)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            isLeftDirectionFacing = true;
            isRigthDirectionFacing = false;

            //Debug.Log($"左向き R：{isRigthDirectionFacing} L：{isLeftDirectionFacing} Rotate：{transform.rotation}");
        }


        animator.SetBool(PlayerAnimationTriggerName.MoveBool.ToString(), true);

        Debug.Log("Moveアニメーション");
    }

    public void PlayerDieAnimation()
    {
        animator.SetTrigger(PlayerAnimationTriggerName.DieTrigger.ToString());
    }
}
