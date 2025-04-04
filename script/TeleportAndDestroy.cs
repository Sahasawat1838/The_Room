using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnly : MonoBehaviour
{
    public GameObject objectToTeleport; // วัตถุที่ต้องการให้ teleport
    public Vector3 teleportPosition; // ตำแหน่งที่ต้องการ teleport
    private bool hasTriggered = false; // ตัวแปรเช็คว่า trigger เกิดขึ้นแล้วหรือยัง

    // เมื่อผู้เล่นเข้ามาใน Trigger
    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่า object ที่เข้ามาคือผู้เล่น (Player) และ trigger ยังไม่เกิดขึ้น
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // ซ่อนวัตถุ (Enemy) ก่อน
            objectToTeleport.SetActive(false);

            // เปลี่ยนตำแหน่งของวัตถุไปยังตำแหน่งใหม่
            objectToTeleport.transform.position = teleportPosition;

            // แสดงวัตถุ (Enemy) ขึ้นมาอีกครั้ง
            objectToTeleport.SetActive(true);

            // ตั้งค่าตัวแปร hasTriggered ให้เป็น true เพื่อไม่ให้ trigger เกิดขึ้นอีก
            hasTriggered = true;
        }
    }
}