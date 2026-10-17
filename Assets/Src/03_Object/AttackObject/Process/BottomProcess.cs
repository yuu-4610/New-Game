using UnityEngine;

public class BottomProcess
{
    private GameObject bottomObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private string trantitionTagName;
    private float attackPower = default;
    private int followObjectDistanceLength = 6;


    public BottomProcess(Animator animator, GameObject bottomObject)
    {
        this.animator = animator;
        this.bottomObject = bottomObject;

    }

    private void TargetObjectFollow()
    {
        //X,Y座標を確定する（１の位、小数点、正/負の数）
        var randomOnespPlaceX = (Random.Range(0, followObjectDistanceLength) + Random.Range(0f, 1f)) * ((Random.Range(0, 2) == 0) ? -1 : 1);
        var randomOnespPlaceY = (Random.Range(0, followObjectDistanceLength) + Random.Range(0f, 1f)) * ((Random.Range(0, 2) == 0) ? -1 : 1);

        Debug.Log($"(x, y)＝({randomOnespPlaceX}, {randomOnespPlaceY})");

        bottomObject.transform.position = new Vector3(
            followObjectTransform.position.x + randomOnespPlaceX,
            followObjectTransform.position.y + randomOnespPlaceY,
            followObjectTransform.position.z);
    }

    public void Attack()
    {
        //座標を決める
        TargetObjectFollow();
        //アニメーションを開始
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
