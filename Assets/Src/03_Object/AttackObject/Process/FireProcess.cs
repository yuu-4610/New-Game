using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FireProcess
{
    private GameObject fireObject;
    private Transform followObjectTransform;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo; //アニメーションの状態
    private bool isActive = default;
    private bool isStart = default;
    private string trantitionTagName;
    private string trantitionTagName_2;
    private float speed = default;
    private float attackPower = default;
    private float startPositionX;
    private Vector3 amountOfMovement; //移動量
    private int directionX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public FireProcess(Animator animator, GameObject fireObject)
    {
        this.animator = animator;
        this.fireObject = fireObject;

    }

    public void TargetObjectFollow()
    {
        if (isActive)
        {
            if (isStart)
            {
                //座標決め
                fireObject.transform.position = followObjectTransform.transform.position;
                //右方向であれば
                directionX = (followObjectTransform.rotation.y <= 0) ? 1 : -1;
                fireObject.transform.rotation = Quaternion.Euler(0, (directionX == 1) ? 0 : 180, 0);
            }
            amountOfMovement += Vector3.right * directionX * speed * Time.deltaTime;
            fireObject.transform.position = amountOfMovement;
            
            
        }
        else
        {
            isStart = false;
        }

        if (amountOfMovement.x > 9f)
        {
            ChangeAnimation();
        }
    }

    // Update is called once per frame
    public void Attack()
    {
        isActive = true;
        isStart = true;
        animator.SetBool(trantitionTagName, true);
    }
    public void SetParameter(float speed, float power, string animationTriggerName, string animationTriggerName_2)
    {
        this.attackPower = power;
        this.speed = speed;
        SetTagName(animationTriggerName, animationTriggerName_2);
    }

    public void InitializeProcess(GameObject playerObject, int objectIndex)
    {
        followObjectTransform = playerObject.transform;
    }

    private void SetTagName(string trantitionTagName, string trantitionTagName_2)
    {
        this.trantitionTagName = trantitionTagName;
        this.trantitionTagName_2 = trantitionTagName_2;
    }
    private void ChangeAnimation()
    {
        isActive = false;
        //火の玉状態で衝突したら
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsName(AttackEffectAnimatorName.Attack.ToString()))
        {
            animator.SetBool(trantitionTagName, false);
        }

        amountOfMovement = Vector3.zero;

        //fireObject.transform.position = Vector3.zero;
    }

    public void CollisionEnemy(Collider2D collision)
    {
        ChangeAnimation();
        if (collision.TryGetComponent<IEnemy>(out var damageable))
        {
            damageable.TakeDamage(attackPower);
        }
    }
}
