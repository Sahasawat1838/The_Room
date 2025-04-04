using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NjidController : MonoBehaviour
{
    public Transform[] TargetPoint;
    public int CurrentPoint;

    public NavMeshAgent AgentJId;
    public Animator nJidanimator;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public enum AIState
    {
        isDead, isSeekTargetPoint, isSeekPlayer
    }

    public AIState state;

    // Start is called before the first frame update
    void Start()
    {
        waitCounter = waitAtPoint;
    }

    // Update is called once per frame
    void Update()
    {
        float DistanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        // เช็คว่าผู้เล่นอยู่ในระยะที่กำหนดหรือไม่
        if (DistanceToPlayer <= 100f)
        {
            state = AIState.isSeekPlayer; // หากผู้เล่นอยู่ใกล้ ให้เริ่มเดินไปหาผู้เล่น
        }
        else
        {
            state = AIState.isSeekTargetPoint; // หากผู้เล่นอยู่ไกล ให้เดินตามจุดเป้าหมาย
        }

        switch (state)
        {
            case AIState.isDead:
                // ไม่มีการทำงานในกรณีนี้
                break;
            case AIState.isSeekPlayer:
                AgentJId.SetDestination(PlayerController.instance.transform.position); // กำหนดจุดหมายให้ไปที่ผู้เล่น
                nJidanimator.SetBool("Run", true); // ทำให้ศัตรูวิ่งตามผู้เล่น

                // หมุนศัตรูไปทางผู้เล่นทันที
                Vector3 directionToPlayer = (PlayerController.instance.transform.position - transform.position).normalized;
                if (directionToPlayer != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(directionToPlayer); // หมุนไปยังทิศทางของผู้เล่น
                }

                // เมื่อศัตรูถึงตัวผู้เล่น
                if (DistanceToPlayer <= 20f) // ระยะที่ต้องการ
                {
                    state = AIState.isDead; // เมื่อถึงผู้เล่นให้เข้าสู่สถานะ dead หรือหยุดทำการอะไรเพิ่มเติม
                    AgentJId.isStopped = true; // หยุดการเคลื่อนไหว
                    nJidanimator.SetBool("Run", false); // หยุดการแสดงอนิเมชั่นการวิ่ง
                }
                break;
            case AIState.isSeekTargetPoint:
                AgentJId.SetDestination(TargetPoint[CurrentPoint].position); // เดินไปยังจุดเป้าหมาย

                if (AgentJId.remainingDistance <= .2f)
                {
                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime; // หยุดรอที่จุดเป้าหมาย
                        nJidanimator.SetBool("Run", false); // หยุดวิ่ง
                    }
                    else
                    {
                        CurrentPoint++; // เลือกจุดถัดไป
                        waitCounter = waitAtPoint; // รีเซ็ตเวลารอ
                        nJidanimator.SetBool("Run", true); // เริ่มวิ่งไปยังจุดถัดไป
                    }

                    if (CurrentPoint >= TargetPoint.Length)
                    {
                        CurrentPoint = 0; // หากถึงจุดสุดท้ายก็เริ่มจากจุดแรก
                    }
                    AgentJId.SetDestination(TargetPoint[CurrentPoint].position); // ตั้งเป้าหมายใหม่
                }
                break;
        }
    }
}