using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; 

public class ending : MonoBehaviour
{
    public GameObject cutSceneObject; 
    public VideoPlayer videoPlayer;    
    private bool isFromEnemy = false;

  
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))  
        {
           
            PlayCutScene();
        }
    }

    
    void PlayCutScene()
    {
        
        if (cutSceneObject != null)
        {
            cutSceneObject.SetActive(true);  
        }

       
        if (videoPlayer != null && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();  
        }
        
        Time.timeScale = 0;
        
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    
    private void OnVideoFinished(VideoPlayer vp)
    {
        EndCutScene();
    }

    
    public void EndCutScene()
    {
        
        CheckWinner();

        Time.timeScale = 1;

        cutSceneObject.SetActive(false);

        SceneManager.LoadScene("MainMenu"); 
    }


    void CheckWinner()
    {
        if (PlayerController.instance != null)
        {
            
            if (isFromEnemy)
            {
                Debug.Log("Player loses!");
            }

            else
            {
                Debug.Log("Player wins!");
            }
        }
    }

    public void SetFromEnemy(bool value)
    {
        isFromEnemy = value;
    }
}
