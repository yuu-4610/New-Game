using UnityEngine;

public class MagicCircleProcess
{
    private GameObject magicCircleObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private bool isAnime = default;
    private string trantitionTagName;
    private float attackPower = default;
    

    public MagicCircleProcess(Animator animator, GameObject magicCircleObject)
    {
        this.animator = animator;
        this.magicCircleObject = magicCircleObject;
    }

    public void TargetObjectFollow()
    {
        magicCircleObject.transform.position = followObjectTransform.position;
    }

    public void Attack(bool isAttack)
    {
        isAnime = (isAttack) ? true : false;
        if (isAnime)
        {
            animator.SetBool(trantitionTagName, true);
            isAnime = false;
        }
        else
        {
            animator.SetBool(trantitionTagName, false);
            isAnime = false;
        }
    }

    public void SetParameter(float power, string animationTriggerName)
    {
        this.attackPower = power;
        if (trantitionTagName != "") animator.ResetTrigger(trantitionTagName);
        SetTagName(animationTriggerName);
    }

    public void SetPlayerObjectReference(GameObject playerObject)
    {
        followObjectTransform = playerObject.transform;
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