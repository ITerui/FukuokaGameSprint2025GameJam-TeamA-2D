using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTest: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnpushButton()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //�Q�[���I��
#else
Application.Quit();  //�Q�[���I��
#endif
        }
    }


