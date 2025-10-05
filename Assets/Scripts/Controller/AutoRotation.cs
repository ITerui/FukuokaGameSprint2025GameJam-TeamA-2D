using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour
{
    public float speedX = 1.0f;
    public float speedY = 1.5f;
    public float speedZ = 2.0f;

    public float amountX = 30.0f;
    public float amountY = 45.0f;
    public float amountZ = 60.0f;

    private Vector3 initialRotation;

    void Start()
    {
        // �I�u�W�F�N�g�̏�����]�p��ۑ�
        initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speedX) * amountX;
        float y = Mathf.Sin(Time.time * speedY) * amountY;
        float z = Mathf.Sin(Time.time * speedZ) * amountZ;

        // �e����sin�g��]��������
        transform.rotation = Quaternion.Euler(initialRotation + new Vector3(x, y, z));
    }
}
