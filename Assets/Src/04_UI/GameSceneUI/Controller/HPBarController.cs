using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HPBarController : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider hpSlider;
    private Camera uiCamera;
    private RectTransform barObjectRectTransform;
    private GameObject targetObject;
    private GameObject childGroupParentObject;
    private Vector3 screenPocition;
    private Vector3 screenPosition = default;
    private float countTime;
    private bool isPositionSet = default;
    private bool isBarActive = default;
    private float barDistance = 0.8f;

    private void OnEnable()
    {
        StartCoroutine(EventRegist());
    }
    private void OnDisable()
    {
        //EventManager.Instance.playerRestHpBarSetting -= BarRestHPSetting;
    }

    private void Update()
    {
        if(isPositionSet)
        {
            //ワールド座標ー＞スクリーン座標へ返還
            screenPocition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y - barDistance, targetObject.transform.position.z);
            Debug.Log($"名前：{barObjectRectTransform.parent.name}");

            screenPosition = uiCamera.WorldToScreenPoint(screenPocition);

            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
                barObjectRectTransform.parent as RectTransform,
                screenPosition, 
                null, //RenderModeがScreenSpace-OverlayならNull
                out var uiLocalPos //ローカル座標の変数
                ))
            {
                barObjectRectTransform.localPosition = uiLocalPos;
            }
        }
        if (isBarActive)
        {
            countTime += Time.deltaTime;
            //指定時間経過後非表示にする
            if(countTime >= 1f)
            {
                isBarActive = false;
                //isPositionSet = false;
                countTime = 0;
                //childGroupParentObject.SetActive(false);
            }
        }
    }
    public void Initialize(GameObject targetObject, Camera uiCamera, int maxHP)
    {
        this.targetObject = targetObject;
        this.uiCamera = uiCamera;

        HPBarSetting(maxHP);
        childGroupParentObject = this.gameObject.transform.Find(ReferenceUIObjectName.ParentObject.ToString()).gameObject;
        barObjectRectTransform = this.gameObject.GetComponent<RectTransform>();

        childGroupParentObject.SetActive(false);
    }
    public void RestHPBarSetting(int restHP)
    {
        hpSlider.value = restHP;

        isBarActive = true;
        countTime = 0; //攻撃を受けた時、表示時間をリセット
        isPositionSet = true;
        childGroupParentObject.SetActive(true);
    }

    public void HPBarSetting(int maxHP)
    {
        Debug.Log($"HPバーmaxHP：{maxHP}");
        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
    }

    private IEnumerator EventRegist()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }

        EventManager.Instance.playerRestHpBarSetting+= RestHPBarSetting;
    }
}
