using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttontest: MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] int selectedIndex;
    [SerializeField] private Fademanager fadeManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per fram
        public void OnPushedButton()
    {
        SeManager seManager = SeManager.Instance;
        seManager.SettingPlaySE();
        fadeManager.FadeToScene("GameScene");
    }
}

