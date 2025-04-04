using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkshake : MonoBehaviour
{
    public float shakeAmount = 0.01f;
    public float shakeFrequency = 3f;
    private Vector3 originalPosition;
    private float playerSpeed = 0f;



    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // ใช้ความเร็วในการเดินของตัวละครมาควบคุมการสั่น
        float shakeOffset = Mathf.Sin(Time.time * shakeFrequency) * shakeAmount * (playerSpeed / 5f);
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + shakeOffset, originalPosition.z);
    }

    // ฟังก์ชันเพื่อรับความเร็วจาก PlayerController
    public void SetSpeed(float speed)
    {
        playerSpeed = speed;
    }
}


