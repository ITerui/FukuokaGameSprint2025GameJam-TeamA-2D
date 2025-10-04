using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameToResult : MonoBehaviour
{
    [SerializeField] private FadeManager fadeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene1");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene2");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene3");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene4");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene5");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene6");
            }
        }
    }
}
