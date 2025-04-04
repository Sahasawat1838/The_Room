using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditMenu : MonoBehaviour
{
   
    public void OnExitButtonClicked()
    {
        Debug.Log("Exit Button Clicked in Credit Menu");

       
        SceneManager.LoadScene("MainMenu"); 
    }
}