using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    public GameObject[] objectsToHide; 
    public AudioClip soundEffect;
    private AudioSource audioSource; 
    private bool hasTriggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

       
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("Player entered the trigger zone!"); 
            HideObjects(); 
            PlaySound();
            hasTriggered = true; 
        }
    }

    void HideObjects()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(false); 
                Debug.Log("Hiding object: " + obj.name); 
            }
        }
    }

    public void ShowObjects()
    {
        foreach (GameObject obj in objectsToHide)
        {
            if (obj != null)
            {
                obj.SetActive(true); 
                Debug.Log("Showing object: " + obj.name); 
            }
        }
    }

    void PlaySound()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect); 
            Debug.Log("Playing sound: " + soundEffect.name);
        }
        else
        {
            Debug.LogWarning("Sound effect or AudioSource not set!"); 
        }
    }
}
