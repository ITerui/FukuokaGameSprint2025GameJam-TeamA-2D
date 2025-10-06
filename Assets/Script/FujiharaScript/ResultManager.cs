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

    [SerializeField] private Image dogImage;  // ���̐i���C���X�g
    [SerializeField] private Image catImage;  // �L�̐i���C���X�g

    [SerializeField] private Sprite[] dogWinSprites;  // ���̏����X�v���C�g
    [SerializeField] private Sprite[] catWinSprites;  // �L�̏����X�v���C�g
    [SerializeField] private Sprite[] dogLoseSprites;  // ���̕����X�v���C�g
    [SerializeField] private Sprite[] catLoseSprites;  // �L�̕����X�v���C�g

    // �Q�[���V�[������n���ꂽ�i���i�K
    public static int dogEvolutionStage = 0;
    public static int catEvolutionStage = 0;
    private int battleResult = 0;

    private Color unselectedColor = new Color(0.3f, 0.3f, 0.3f); // �Â߂̃O���[
    private Color defaultNormalColor;

    private int selectedIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // PlayerPrefs����i���i�K�Ə��s�����擾
        dogEvolutionStage = PlayerPrefs.GetInt("DogEvolutionLevel", 1); // �f�t�H���g�l��1
        catEvolutionStage = PlayerPrefs.GetInt("CatEvolutionLevel", 1); // �f�t�H���g�l��1

        battleResult = PlayerPrefs.GetInt("BattleResult", 0); // �f�t�H���g�l��0�i���������j

        // �摜�̍X�V����
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

        // ���ʂɉ����Č��ƔL�̐i���i�K�𔽉f
        if (battleResult == 1)  // ������
        {
            dogImage.sprite = dogWinSprites[dogEvolutionStage];
            catImage.sprite = catLoseSprites[catEvolutionStage];
        }
        else if (battleResult == -1)  // �L����
        {
            dogImage.sprite = dogLoseSprites[dogEvolutionStage];
            catImage.sprite = catWinSprites[catEvolutionStage];
        }
        else  // ��������
        {
            dogImage.sprite = dogWinSprites[dogEvolutionStage];
            catImage.sprite = catWinSprites[catEvolutionStage];
        }
    }

    private void SetImageSize(Image image, int evolutionStage)
    {
        // �T�C�Y�͐i���i�K�Ɋ�Â��Đݒ�i��j
        // �i���i�K�ɉ����āA�T�C�Y�𒲐��B�����ł͒P���ɒi�K���Ƃɔ{����ς��܂��B
        float sizeMultiplierX = 1f;
        float sizeMultiplierY = 1f;

        switch (evolutionStage)
        {
            case 1:
                sizeMultiplierX = 1.25f;  // �����T�C�Y
                sizeMultiplierY = 2f;  // �����T�C�Y
                break;
            case 2:
                sizeMultiplierX = 1.875f;  // 2�i�K�ڂ�1.5�{
                sizeMultiplierY = 3f;  // 2�i�K�ڂ�1.5�{
                break;
            case 3:
                sizeMultiplierX = 2.5f;  // 3�i�K�ڂ�2�{
                sizeMultiplierY = 4f;  // 3�i�K�ڂ�2�{
                break;
            default:
                sizeMultiplierX = 1f;  // �f�t�H���g��1�{
                sizeMultiplierY = 1f;  // �f�t�H���g��1�{
                break;
        }

        // RectTransform�̃T�C�Y��ݒ�
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(100f * sizeMultiplierX, 100f * sizeMultiplierY);  // 100�͊�{�T�C�Y�i�����\�j
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