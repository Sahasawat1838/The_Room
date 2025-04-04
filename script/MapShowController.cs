using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowController : MonoBehaviour
{
    // ตัวแปรที่ใช้เก็บ GameObjects ที่ต้องการแสดง
    public GameObject[] objectsToShow; // ตัวแปรอาร์เรย์ที่ใช้เก็บ GameObject ที่จะแสดง

    // ตัวแปรที่ใช้เก็บ GameObjects ที่ต้องการทำลาย
    public GameObject[] objectsToDestroy; // ตัวแปรอาร์เรย์ที่ใช้เก็บ GameObject ที่จะทำลาย

    void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าเป็นผู้เล่นที่เข้ามาใน Trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone!"); // ตรวจสอบว่า Trigger ทำงาน
            ShowObjects(); // แสดงวัตถุที่เลือกเมื่อผู้เล่นเดินผ่าน Trigger
            DestroyObjects(); // ทำลายวัตถุที่เลือก
        }
    }

    // ฟังก์ชันที่ใช้แสดง GameObject ที่ระบุใน objectsToShow
    void ShowObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(true); // แสดง GameObject
                Debug.Log("Showing object: " + obj.name); // แสดงชื่อของ GameObject ที่แสดง
            }
        }
    }

    // ฟังก์ชันที่ใช้ทำลาย GameObject ที่ระบุใน objectsToDestroy
    void DestroyObjects()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj); // ทำลาย GameObject
                Debug.Log("Destroying object: " + obj.name); // แสดงชื่อของ GameObject ที่ถูกทำลาย
            }
        }
    }

    // ฟังก์ชันสำหรับการซ่อน GameObject
    public void HideObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(false); // ซ่อน GameObject
                Debug.Log("Hiding object: " + obj.name); // แสดงชื่อของ GameObject ที่ถูกซ่อน
            }
        }
    }
}
