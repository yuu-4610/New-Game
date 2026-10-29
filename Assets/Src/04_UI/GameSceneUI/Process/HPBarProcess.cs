using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class HPBarProcess
{
    private Slider playerPositionHPBar; //プレイヤーに追跡するHPバー
    private Slider playerHPBar; //プレイヤーに追跡するHPバー
    private Image restHp;

    public HPBarProcess(GameObject playerPositionHPBar, Slider playerHPBar)
    {
        this.playerHPBar = playerHPBar;
        //this.playerPositionHPBar = playerPositionHPBar.GetComponent<Slider>();
    }

    public void HpBarInitialize()
    {
        //BarSetting(playerPositionHPBar);
        BarSetting(playerHPBar);
    }
    public void BarHpSetting(int maxHP)
    {
        //playerPositionHPBar.maxValue = maxHP;
        //playerPositionHPBar.value = maxHP;
        playerHPBar.maxValue = maxHP;
        playerHPBar.value = maxHP;
    }
    public void BarRestHPSetting(int restHp)
    {
        //playerPositionHPBar.value = restHp;
        playerHPBar.value = restHp;
        if(playerHPBar.value <= 0) playerHPBar.transform.Find(ReferenceUIObjectName.FillArea.ToString()).gameObject.SetActive(false);
    }

    private void BarSetting(Slider slider)
    {
        slider.interactable = true;

        //ダメージ量部分の色の設定
        slider.targetGraphic = slider.transform.Find(ReferenceUIObjectName.Background.ToString()).GetComponent<Graphic>();
        ColorBlock colors = slider.colors;
        colors.normalColor = UnityEngine.Color.red;
        slider.colors = colors;

        //残HP部分の色の設定
        restHp = slider.transform.Find(ReferenceUIObjectName.FillArea.ToString()).transform.Find(ReferenceUIObjectName.Fill.ToString()).GetComponent<Image>();
        restHp.color = new UnityEngine.Color(0.4080103f, 1f, 0.05849051f, 1f);
    }
    public void BarPosition(Vector3 position)
    {
        //playerPositionHPBar.transform.position = position;
    }
}
