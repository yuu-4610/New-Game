using UnityEngine;

public class lightningProcess
{
    private GameObject lightningObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private Vector3 randomOnespPlace;
    private string trantitionTagName;
    private float attackPower = default;
    private int followObjectDistanceLength = 4;
    private int attackPatternIndex = default;


    public lightningProcess(Animator animator, GameObject lightningObject)
    {
        this.animator = animator;
        this.lightningObject = lightningObject;

    }

    private void TargetObjectFollow()
    {
        if(attackPatternIndex == 1)
        {
            //X,Y座標を確定する（１の位、小数点、正/負の数）
            randomOnespPlace = new Vector3(
                (Random.Range(0, followObjectDistanceLength) + Random.Range(0f, 1f)) * ((Random.Range(0, 2) == 0) ? -1 : 1),
                (Random.Range(0, followObjectDistanceLength) + Random.Range(0f, 1f)) * ((Random.Range(0, 2) == 0) ? -1 : 1) + (lightningObject.transform.localScale.y / 2),
                0);
            //var randomOnespPlaceY = 6 + (thunderObject.transform.localScale.y / 2);
        }

        Debug.Log($"(x, y)＝({randomOnespPlace.x}, {randomOnespPlace.y})");

        lightningObject.transform.position = new Vector3(
            followObjectTransform.position.x + randomOnespPlace.x,
            followObjectTransform.position.y + randomOnespPlace.y,
            followObjectTransform.position.z);
    }

    public void Attack()
    {
        TargetObjectFollow();
        animator.SetTrigger(trantitionTagName);
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
        attackPatternIndex =
            trantitionTagName == AttackEffectAnimationTriggerName.AttackTrigger.ToString() ? 1 :
            0;
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