using UnityEngine;

public class ArmorSkeletonController : StateMachine<ArmorSkeletonController, ArmorSkeletonState>
{
    private ArmorSkeletonViewController armorSkeletonViewController;


    private void Awake()
    {
        armorSkeletonViewController = GetComponent<ArmorSkeletonViewController>();

        //Stateクラスの追加
        stateList.Add(new ArmorSkeletonStateIdle(this, armorSkeletonViewController));
        stateList.Add(new ArmorSkeletonStateMove(this, armorSkeletonViewController));
        stateList.Add(new ArmorSkeletonStateDie(this, armorSkeletonViewController));
    }
    void Start()
    {
        ChangeStateCall(ArmorSkeletonState.Idle);
    }

    public override void Update()
    {
        base.Update();
    }
    public void ChangeStateCall(ArmorSkeletonState armorSkeletonState)
    {
        base.ChangeState(stateList[(int)armorSkeletonState]);
    }
}