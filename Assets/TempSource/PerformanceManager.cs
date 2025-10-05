using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class PerformanceManager : MonoBehaviour
{
    public GameObject AttackPerformancePannel;
    public Canvas HUD;
    public RectTransform p1Trans;
    public RectTransform p2Trans;

    public float speed = 100f;
    private bool flag = false;

    private Vector3 SaveLoc1;
    private Vector3 SaveLoc2;
    private float distance = 0f;

    public void InitLoc()
    {
        SaveLoc1 = p1Trans.position;
        SaveLoc2 = p2Trans.position;
    }

    public void OnAttackPerformance(float inDistance)
    {
        distance = inDistance;
        AttackPerformancePannel.SetActive(true);
        flag = true;
        //HUD.gameObject.SetActive(false);
        StartCoroutine(MoveForward());
    }

    public void EndAttackPerformance()
    {
        AttackPerformancePannel.SetActive(false);
        //HUD.gameObject.SetActive(true);
        StopCoroutine(MoveForward());
        p1Trans.position = SaveLoc1;
        p2Trans.position = SaveLoc2;
        flag = false;
        distance = 0f;
    }
    private IEnumerator MoveForward()
    {
        while (true)
        {
            if (!flag) break;

            // deltaTime�������ăt���[���ˑ����Ȃ��ړ��ʂ��v�Z
            Vector3 moveVector = p1Trans.right * speed * Time.deltaTime;

            // RectTransform�̈ʒu���ړ�
            p1Trans.position += moveVector * distance;
            p2Trans.position += moveVector * distance;

            yield return null; // ���̃t���[���܂őҋ@
        }
    }
}
