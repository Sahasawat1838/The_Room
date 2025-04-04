using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEventTrigger : MonoBehaviour
{
    public enum EventState
    {
        Idle,
        EventOne,
        EventTwo,
        EventThree
    }

    public EventState currentState = EventState.Idle;

    // ตัวแปรนี้เก็บการอ้างอิงไปยัง MapController
    public MapController mapController;

    void Start()
    {
        if (mapController == null)
        {
            mapController = FindObjectOfType<MapController>(); // ค้นหา MapController ใน Scene
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandleEventState();
        }
    }

    void HandleEventState()
    {
        switch (currentState)
        {
            case EventState.Idle:
                // ไม่ทำอะไร
                break;
            case EventState.EventOne:
                TriggerEventOne();
                break;
            case EventState.EventTwo:
                TriggerEventTwo();
                break;
            case EventState.EventThree:
                TriggerEventThree();
                break;
        }
    }

    void TriggerEventOne()
    {
        // Event 1: Spawn Enemies
        Debug.Log("Event One: Spawning Enemies!");
        // สร้างศัตรู
    }

    void TriggerEventTwo()
    {
        // Event 2: Unlock a door
        Debug.Log("Event Two: Unlocking Door!");
        // ปลดล็อกประตู
    }

    void TriggerEventThree()
    {
        // Event 3: Give a reward
        Debug.Log("Event Three: Giving Reward!");
        // ให้รางวัล

        // ตรวจสอบว่า mapController ไม่เป็น null ก่อนเรียก CreateMap()
        if (mapController != null)
        {
            //mapController.CreateMap(); // สร้างแผนที่
        }
        else
        {
            Debug.LogError("MapController not assigned!");
        }
    }

    public void SetEventState(EventState newState)
    {
        currentState = newState; // เปลี่ยนสถานะห้อง
    }

}


