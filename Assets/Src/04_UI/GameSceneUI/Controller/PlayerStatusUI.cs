using System.Collections;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] GameObject playerPositionHPBar; //プレイヤーに追跡するHPバー
    [SerializeField] Slider playerHPBar; //プレイヤーに追跡するHPバー
    private HPBarProcess hpBarProcess;
    private GameObject HPBar;
    private GameObject player;

    void Start()
    {
        //HPBar = Instantiate(playerPositionHPBar, this.gameObject.transform);

        hpBarProcess = new HPBarProcess(HPBar, playerHPBar);
    }
    private void Update()
    {
        if(player != null)
        {
            //hpBarProcess.BarPosition(player.transform.position);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(EventRegister());
    }
    private void OnDisable()
    {
        EventManager.Instance.finishedGeneratePlayer -= BarInitialize;
    }

    private void BarInitialize()
    {
        //UI初期化処理
        hpBarProcess.HpBarInitialize();
        //hpBarProcess.HpSetting();
    }

    private void Initialize()
    {
        player = ObjectManager.Instance.GetObject(AcquisitionObjectName.Player.ToString());
    }

    private void PlayerHPBarSetting(int maxHP)
    {
        //最大HP（MAXゲージ）の設定
        //想定ー＞レベルアップ時
        hpBarProcess.BarHpSetting(maxHP);
    }
    private void PlayerRestHPBarSetting(int restHP)
    {
        hpBarProcess.BarRestHPSetting(restHP);
    }

    private IEnumerator EventRegister()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.finishedGeneratePlayer += BarInitialize;
        EventManager.Instance.playerHPBarSetting += PlayerHPBarSetting;
        EventManager.Instance.playerRestHpBarSetting += PlayerRestHPBarSetting;
        EventManager.Instance.finishedGeneratePlayer += Initialize;
    }

}
