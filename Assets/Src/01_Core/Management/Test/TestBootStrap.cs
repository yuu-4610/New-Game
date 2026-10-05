using UnityEngine;

public class TestBootStrap : MonoBehaviour
{
    [SerializeField] GameObject TestGameManager;
    [SerializeField] GameObject EventManager;
    //[SerializeField] GameObject AudioManager;
    [SerializeField] GameObject ObjectManagaer;
    [SerializeField] GameObject SceneProcessControllere;
    //[SerializeField] GameObject UIManager;
    [SerializeField] GameObject TestEventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GeneratSingltonObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GeneratSingltonObject()
    {
        Instantiate(TestEventManager);
        Instantiate(EventManager);
        Instantiate(TestGameManager);
        //Instantiate(AudioManager);
        Instantiate(ObjectManagaer);
        Instantiate(SceneProcessControllere);
        //Instantiate(UIManager);
    }
}
