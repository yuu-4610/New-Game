using System.Collections;
using UnityEngine;

/*列挙型で用意し以下の問題を未然に防ぐ
 *・文字列指定した際のスペルミスによるエラーの防止
 *・インデックス指定した際の可読性低下や混乱を防ぐ
 *定義した値のみ使用できるようにして安全に管理するため
 */

//存在するシーン名
public enum SceneName
{
    TitleScene, //タイトルシーン
    GameScene, //ゲームシーン
}

//生成する親オブジェクト名
public enum GenerateParentObjectName
{
    
}

//オブジェクトに設定するtag名
public enum TagName
{
    Piece, //ピースオブジェクトに付与
}


//取得するオブジェクト名
public enum AcquisitionObjectName
{
    ObjectFactory,
}

//Resourcesフォルダから見たAudio資材のパス変数
public enum ResourcePath
{
    BGM, //BGMが置いてあるパス
    SE, //SEが置いてあるパス
    enemyClassStorageLocation, //生成するエネミークラスパス
    importFileStorageLocation,
    enumFileAndStorageLocation, //追記するEnumクラスのパス
}

//AudioMixerのGroupName
public enum AudioMixerGroupName
{
    BGM,
    SE,
}

//Audio資材のファイル名
public enum AudioFileName
{
    
}

public enum poolObjectName
{
    slashEffect,
}


public enum PlayerAnimationTriggerName
{
    MoveBool,
    DieTrigger,
}

public enum SkeletonAnimationTriggerName
{
    MoveBool,
}

public enum SlimeAnimationTriggerName
{
    MoveBool,
}

public enum AttackEffectAnimationTriggerName
{
    AttackBool,
}
public enum AttackEffectAnimatorName
{
    Attack,
}

public enum InporTextFileName
{
    ImportCharactorControllerTemplate,
    ImportCharactorViewControllerTemplate,
    ImportCharactorEnumClassTemplate,
    ImportCharactorStateActionTemplate,
}

public enum RewiteValueName
{
    CLASS_NAME,
    VIEWCLASS_NAME,
    VIEWCLASS_NAME_TOLOWER_CASE,
    ENUMCLASS_NAME,
    STATE_NAME,
    STATE_NAME_TOLOWER_CASE,
    CHARACTOR_NAME,
}

