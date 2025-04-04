using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorKey : MonoBehaviour
{
    
    [SerializeField] bool playerInRange;
    [SerializeField] bool isdooropen;
    [SerializeField] TMP_Text interactText;

    Animator dooranim;
    AudioSource dooraudio;
    //AudioClip doorMoveSound;

    private void Awake()
    {
        interactText.SetText("");
    }

    private void Start()
    {
        dooranim = GetComponent<Animator>();
        dooraudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(playerInRange)
        {
            if(isdooropen)
            {

            }
            else
            {
                interactText.SetText("\"E\" to Open");
            }
        }
        else
        {
            interactText.SetText("");
        }

        if(Input.GetKeyDown(KeyCode.E)&&playerInRange)
        {
            if(isdooropen)
            {
                dooranim.SetTrigger("Close");
                isdooropen = false;
                //dooraudio.PlayOneShot(doorMoveSound);
            }
            else
            {
                dooranim.SetTrigger("Open");
                isdooropen = true;
                //dooraudio.PlayOneShot(doorMoveSound);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("player has entered");
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //print("player has exit");
            playerInRange = false;
        }
    }
}
