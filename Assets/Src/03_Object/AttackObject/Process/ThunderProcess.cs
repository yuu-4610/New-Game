using UnityEngine;

public class ThunderProcess
{
    private GameObject thunderObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private string trantitionTagName;
    private float attackPower = default;
    private int followObjectDistanceLength = 4;


    public ThunderProcess(Animator animator, GameObject thunderObject)
    {
        this.animator = animator;
        this.thunderObject = thunderObject;

    }

    private void TargetObjectFollow()
    {
        //X,Y座標を確定する（１の位、小数点、正/負の数）
        var randomOnespPlaceX = (Random.Range(0, followObjectDistanceLength) + Random.Range(0f, 1f)) * ((Random.Range(0, 2) == 0) ? -1 : 1);
        var randomOnespPlaceY = (Random.Range(0, followObjectDistanceLength) + Random.Range(0f, 1f)) * ((Random.Range(0, 2) == 0) ? -1 : 1) + (thunderObject.transform.localScale.y / 2);
        //var randomOnespPlaceY = 6 + (thunderObject.transform.localScale.y / 2);

        Debug.Log($"(x, y)＝({randomOnespPlaceX}, {randomOnespPlaceY})");

        thunderObject.transform.position = new Vector3(
            followObjectTransform.position.x + randomOnespPlaceX,
            followObjectTransform.position.y + randomOnespPlaceY,
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

    public void InitializeProcess(GameObject playerObject)
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