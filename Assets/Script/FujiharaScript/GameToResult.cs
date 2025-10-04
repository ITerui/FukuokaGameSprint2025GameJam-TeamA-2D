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
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene1");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene2");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene3");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene4");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene5");
            }
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                fadeManager.FadeToScene("ResultScene6");
            }
        }
    }
}
   