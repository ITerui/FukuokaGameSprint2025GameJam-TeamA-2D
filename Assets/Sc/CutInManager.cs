using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutInManager : MonoBehaviour
{
    [Header("UI")]
    public RectTransform player1Image;
    public RectTransform player2Image;
    public Image vsImage;
    public Image darkBackground;

    [Header("���o�ݒ�")]
    public float slideDuration = 0.5f;  // �X���C�h�C���̎���
    public float waitTime = 1.0f;       // VS �\������
    public float slideOffset = 500f;    // �J�n�ʒu�i��ʊO�j
    [Range(0f, 1f)] public float darkAlpha = 0.3f;
    public float slideOutSpeedMultiplier = 2f; // �X���C�h�A�E�g�𑬂�����{��
    public float vsRiseHeight = 200f;   // VS �㏸��

    void Start()
    {
        Vector3 p1Target = player1Image.localPosition;
        Vector3 p2Target = player2Image.localPosition;

        // �J�n�ʒu����ʊO�ɐݒ�
        player1Image.localPosition = p1Target + new Vector3(-slideOffset, 0, 0);
        player2Image.localPosition = p2Target + new Vector3(slideOffset, 0, 0);

        // VS Image ������
        vsImage.transform.localScale = Vector3.zero;
        vsImage.gameObject.SetActive(true);

        // �w�i������
        if (darkBackground != null)
        {
            var col = darkBackground.color;
            col.a = 0f;
            darkBackground.color = col;
        }

        // ���o�J�n
        StartCoroutine(PlayCutIn(p1Target, p2Target));
    }

    IEnumerator PlayCutIn(Vector3 p1Target, Vector3 p2Target)
    {
        // �w�i�Ó]
        if (darkBackground != null)
        {
            var col = darkBackground.color;
            col.a = darkAlpha;
            darkBackground.color = col;
        }

        // �����X���C�h�C��
        yield return StartCoroutine(SlideBoth(player1Image, player2Image, player1Image.localPosition, p1Target, player2Image.localPosition, p2Target, slideDuration));

        // VS �g��e��
        yield return StartCoroutine(PopVS(vsImage));

        yield return new WaitForSeconds(waitTime);

        // �����X���C�h�A�E�g�i�����j + VS ��ɔ�΂�
        float outDuration = slideDuration / slideOutSpeedMultiplier;
        yield return StartCoroutine(SlideOutWithVS(player1Image, player2Image, p1Target, p2Target, outDuration));

        // VS ��\��
        vsImage.gameObject.SetActive(false);

        // �w�i���Z�b�g
        if (darkBackground != null)
        {
            var col = darkBackground.color;
            col.a = 0f;
            darkBackground.color = col;
        }

        // �Q�[���J�n
        //FindObjectOfType<GameManager>().StartNextRound();

        Destroy(gameObject);
    }

    // �����X���C�h
    IEnumerator SlideBoth(RectTransform r1, RectTransform r2, Vector3 from1, Vector3 to1, Vector3 from2, Vector3 to2, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            r1.localPosition = Vector3.Lerp(from1, to1, t);
            r2.localPosition = Vector3.Lerp(from2, to2, t);
            yield return null;
        }
        r1.localPosition = to1;
        r2.localPosition = to2;
    }

    // VS �g��e�މ��o
    IEnumerator PopVS(Image vs)
    {
        float time = 0f;
        float duration = 0.5f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // �e�ރC�[�W���O�i�Ȉ� EaseOutBack + �e�݁j
            float s = t * t * (3f - 2f * t);
            s = s + Mathf.Sin(t * Mathf.PI * 2f) * 0.2f;
            vs.transform.localScale = Vector3.Lerp(startScale, endScale, s);

            yield return null;
        }
        vs.transform.localScale = endScale;
    }

    // �X���C�h�A�E�g + VS ��ɔ�΂�
    IEnumerator SlideOutWithVS(RectTransform r1, RectTransform r2, Vector3 p1Target, Vector3 p2Target, float duration)
    {
        Vector3 r1End = p1Target + new Vector3(-slideOffset, 0, 0);
        Vector3 r2End = p2Target + new Vector3(slideOffset, 0, 0);
        Vector3 vsStart = vsImage.transform.localPosition;
        Vector3 vsEnd = vsStart + new Vector3(0, vsRiseHeight, 0);

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            r1.localPosition = Vector3.Lerp(p1Target, r1End, t);
            r2.localPosition = Vector3.Lerp(p2Target, r2End, t);
            vsImage.transform.localPosition = Vector3.Lerp(vsStart, vsEnd, t);

            yield return null;
        }

        r1.localPosition = r1End;
        r2.localPosition = r2End;
        vsImage.transform.localPosition = vsEnd;
    }
}
