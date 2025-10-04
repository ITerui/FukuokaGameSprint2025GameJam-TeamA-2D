using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] int selectedIndex;
    [SerializeField] private FadeManager fadeManager;

    private Color unselectedColor = new Color(0.3f, 0.3f, 0.3f); // à√ÇﬂÇÃÉOÉåÅ[
    private Color defaultNormalColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultNormalColor = buttons[0].colors.normalColor;
        UpdateButtonColors();

        SelectButton(selectedIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = (selectedIndex + 1) % buttons.Length;
            UpdateButtonColors();
            SelectButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
            UpdateButtonColors();
            SelectButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[selectedIndex].onClick.Invoke();
        }
    }

    void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }

    void UpdateButtonColors()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var colors = buttons[i].colors;

            if (i == selectedIndex)
            {
                colors.normalColor = defaultNormalColor;
            }
            else
            {
                colors.normalColor = unselectedColor;
            }

            buttons[i].colors = colors;
        }
    }

    public void ReStartGame()
    {
        fadeManager.FadeToScene("Test GameScene");
    }

    public void GoTitle()
    {
        fadeManager.FadeToScene("Test TitleScene");
    }
}
