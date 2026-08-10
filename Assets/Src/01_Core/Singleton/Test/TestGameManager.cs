using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameManager : MonoBehaviour
{
    /*<責務>ゲームの進行度の
     */
    public static TestGameManager Instance;
    public static int sceneNumber; //シーン番号、遷移時に仕様

    // Start is called before the first frame update
    private void Awake()
    {
        //シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        //Titleシーン（一番最初のシーン）で配置したオブジェクトを残す
        DontDestroyOnLoad(this.gameObject);

        //イベントの登録
        //シングルトンオブジェクトの生成が順不同であるため、生成を待ちイベント登録する
        StartCoroutine(EventRegistration());
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        EventManager.Instance.transitionGameToResult -= GameFinish;
    }
    void Start()
    {
        //万が一TitleSceneから始まらなかった場合の処理
        SceneTransition(TestSceneName.TestGameScene);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
    }
    public void SceneTransition(TestSceneName sceneType)
    {
        sceneNumber = (int)sceneType;
        //
        if (sceneType == TestSceneName.TestGameScene)
        {
            if(SceneManager.GetActiveScene().name == TestSceneName.TestGameScene.ToString())
            {
                //SceneManager.LoadScene(sceneType.ToString());
            }
            StartCoroutine(SceneProcessMethodCall(sceneNumber));
        }

    }

    //GameScene 終了時の処理
    private void GameFinish()
    {

    }

    //EventManagerが生成されるまで待つ
    private IEnumerator EventRegistration()
    {
        while (EventManager.Instance == null)
        {
            yield return null;
        }
    }

    private IEnumerator SceneProcessMethodCall(int sceneType)
    {
        while (SceneProcessController.Instance == null || !SceneProcessController.Instance.hasEvent)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        //シーン遷移時に処理

        EventManager.Instance.SceneTransitionEvent(sceneType);
    }
}
