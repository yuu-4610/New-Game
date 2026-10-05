using UnityEngine;

public class BootStrap : MonoBehaviour
{
    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject EventManager;
    [SerializeField] GameObject AudioManager;
    [SerializeField] GameObject ObjectManagaer;
    [SerializeField] GameObject SceneProcessControllere;
    [SerializeField] GameObject UIManager;

    

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
        Instantiate(EventManager);
        Instantiate(GameManager);
        Instantiate(AudioManager);
        Instantiate(ObjectManagaer);
        Instantiate(SceneProcessControllere);
        Instantiate(UIManager);
    }
}
