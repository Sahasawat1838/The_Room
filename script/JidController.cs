using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EnemyController : MonoBehaviour
{
    public Transform[] TargetPoint;
    public int CurrentPoint;

    public NavMeshAgent AgentJId;
    public Animator Jidanimator;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public enum AIState
    {
        isDead, isSeekTargetPoint, isSeekPlayer, isInCutscene
    }

    public VideoPlayer cutscenePlayer1;
    public VideoPlayer cutscenePlayer2;

    private VideoPlayer[] cutscenes;

    void Start()
    {
        waitCounter = waitAtPoint;


        cutscenes = new VideoPlayer[] { cutscenePlayer1, cutscenePlayer2 };


        cutscenePlayer1.loopPointReached += OnCutsceneEnd;
        cutscenePlayer2.loopPointReached += OnCutsceneEnd;
    }

    public AIState state;

    // Update is called once per frame
    void Update()
    {
        float DistanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);


        if (DistanceToPlayer <= 100f)
        {
            state = AIState.isSeekPlayer;
        }
        else
        {
            state = AIState.isSeekTargetPoint;
        }

        switch (state)
        {
            case AIState.isDead:
                break;
            case AIState.isSeekPlayer:

                AgentJId.SetDestination(PlayerController.instance.transform.position);
                Jidanimator.SetBool("jidfly", true);


                Vector3 directionToPlayer = (PlayerController.instance.transform.position - transform.position).normalized;
                if (directionToPlayer != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(directionToPlayer); // หมุนไปทางผู้เล่น
                }


                if (DistanceToPlayer <= 20f)
                {
                    state = AIState.isInCutscene;
                    AgentJId.isStopped = true;
                    Jidanimator.SetBool("jidfly", false);
                    PlayRandomCutscene();
                }
                break;
            case AIState.isSeekTargetPoint:
                AgentJId.SetDestination(TargetPoint[CurrentPoint].position);

                if (AgentJId.remainingDistance <= .2f)
                {
                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                        Jidanimator.SetBool("jidfly", false);
                    }
                    else
                    {
                        CurrentPoint++;
                        waitCounter = waitAtPoint;
                        Jidanimator.SetBool("jidfly", true);
                    }

                    if (CurrentPoint >= TargetPoint.Length)
                    {
                        CurrentPoint = 0;
                    }
                    AgentJId.SetDestination(TargetPoint[CurrentPoint].position);
                }
                break;
            case AIState.isInCutscene:

                break;
        }
    }


    void PlayRandomCutscene()
    {

        int randomIndex = Random.Range(0, cutscenes.Length); // เลือก 0 หรือ 1

        // เล่น Cutscene ที่สุ่มเลือก
        cutscenes[randomIndex].Play();
    }


    void OnCutsceneEnd(VideoPlayer vp)
    {

        state = AIState.isSeekTargetPoint;
        AgentJId.isStopped = false;


        SceneManager.LoadScene("MainMenu");
    }
}