using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWinner : MonoBehaviour
{
    public static CheckWinner instance;  // Singleton pattern
    public bool isWinner = false;        // ตัวแปรที่ใช้บ่งชี้ว่าใครเป็นผู้ชนะ

    // ฟังก์ชันที่ใช้สำหรับตั้งค่าผลการชนะ
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // ตัวอย่างฟังก์ชันที่ใช้ในการตั้งค่าผู้เล่นเป็นผู้ชนะ
    public void SetWinner()
    {
        isWinner = true;
        //PlayerController.instance.isWinner = true;  // อัปเดตตัวแปรใน PlayerController
    }

    // ฟังก์ชันอื่น ๆ สำหรับตรวจสอบเกม
}
