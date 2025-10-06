using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResultManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonImageSet
    {
        public Button button;
        public Sprite selectedSprite;
        public Sprite unselectedSprite;
    }

    [SerializeField] private AudioClip buttonSE;
    private AudioSource audioSource;

    [SerializeField] private ButtonImageSet[] buttonImageSets;
    [SerializeField] private FadeManager fadeManager;

    [SerializeField] private Image dogImage;  // 犬の進化イラスト
    [SerializeField] private Image catImage;  // 猫の進化イラスト

    [SerializeField] private Sprite[] dogWinSprites;  // 犬の勝ちスプライト
    [SerializeField] private Sprite[] catWinSprites;  // 猫の勝ちスプライト
    [SerializeField] private Sprite[] dogLoseSprites;  // 犬の負けスプライト
    [SerializeField] private Sprite[] catLoseSprites;  // 猫の負けスプライト

    // ゲームシーンから渡された進化段階
    public static int dogEvolutionStage = 0;
    public static int catEvolutionStage = 0;
    private int battleResult = 0;

    private Color unselectedColor = new Color(0.3f, 0.3f, 0.3f); // 暗めのグレー
    private Color defaultNormalColor;

    private int selectedIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // PlayerPrefsから進化段階と勝敗情報を取得
        dogEvolutionStage = PlayerPrefs.GetInt("DogEvolutionLevel", 1); // デフォルト値は1
        catEvolutionStage = PlayerPrefs.GetInt("CatEvolutionLevel", 1); // デフォルト値は1

        battleResult = PlayerPrefs.GetInt("BattleResult", 0); // デフォルト値は0（引き分け）

        // 画像の更新処理
        UpdateDogCatImages();

        UpdateButtonImages();
        SelectButton(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = (selectedIndex + 1) % buttonImageSets.Length;
            UpdateButtonImages();
            SelectButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = (selectedIndex - 1 + buttonImageSets.Length) % buttonImageSets.Length;
            UpdateButtonImages();
            SelectButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(buttonSE);
            buttonImageSets[selectedIndex].button.onClick.Invoke();
        }
    }

    void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(buttonImageSets[index].button.gameObject);
    }

    void UpdateButtonImages()
    {
        for (int i = 0; i < buttonImageSets.Length; i++)
        {
            Image buttonImage = buttonImageSets[i].button.GetComponent<Image>();
            if (i == selectedIndex)
            {
                buttonImage.sprite = buttonImageSets[i].selectedSprite;
            }
            else
            {
                buttonImage.sprite = buttonImageSets[i].unselectedSprite;
            }
        }
    }

    private void UpdateDogCatImages()
    {
        SetImageSize(dogImage, dogEvolutionStage);
        SetImageSize(catImage, catEvolutionStage);

        // 結果に応じて犬と猫の進化段階を反映
        if (battleResult == 1)  // 犬勝ち
        {
            dogImage.sprite = dogWinSprites[dogEvolutionStage];
            catImage.sprite = catLoseSprites[catEvolutionStage];
        }
        else if (battleResult == -1)  // 猫勝ち
        {
            dogImage.sprite = dogLoseSprites[dogEvolutionStage];
            catImage.sprite = catWinSprites[catEvolutionStage];
        }
        else  // 引き分け
        {
            dogImage.sprite = dogWinSprites[dogEvolutionStage];
            catImage.sprite = catWinSprites[catEvolutionStage];
        }
    }

    private void SetImageSize(Image image, int evolutionStage)
    {
        // サイズは進化段階に基づいて設定（例）
        // 進化段階に応じて、サイズを調整。ここでは単純に段階ごとに倍率を変えます。
        float sizeMultiplierX = 1f;
        float sizeMultiplierY = 1f;

        switch (evolutionStage)
        {
            case 1:
                sizeMultiplierX = 1.25f;  // 初期サイズ
                sizeMultiplierY = 2f;  // 初期サイズ
                break;
            case 2:
                sizeMultiplierX = 1.875f;  // 2段階目は1.5倍
                sizeMultiplierY = 3f;  // 2段階目は1.5倍
                break;
            case 3:
                sizeMultiplierX = 2.5f;  // 3段階目は2倍
                sizeMultiplierY = 4f;  // 3段階目は2倍
                break;
            default:
                sizeMultiplierX = 1f;  // デフォルトは1倍
                sizeMultiplierY = 1f;  // デフォルトは1倍
                break;
        }

        // RectTransformのサイズを設定
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100f * sizeMultiplierX, 100f * sizeMultiplierY);  // 100は基本サイズ（調整可能）
    }

    public void ReStartGame()
    {
        fadeManager.FadeToScene("MainGameScene");
    }

    public void GoTitle()
    {
        fadeManager.FadeToScene("TitleScene");
    }
}