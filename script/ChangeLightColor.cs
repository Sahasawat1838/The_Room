using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightColor : MonoBehaviour
{
    public Color newColor = Color.red;
    public string playerTag = "Player";
    public bool changeAllLights = true;

    
    public List<GameObject> groupObjects = new List<GameObject>(); 
    public List<Light> specificLights = new List<Light>(); 

   
    private void OnTriggerEnter(Collider other) 
    {
        
        if (other.CompareTag(playerTag))
        {
            
            if (changeAllLights)
            {
                foreach (GameObject groupObject in groupObjects)
                {
                    
                    Light[] lights = groupObject.GetComponentsInChildren<Light>();

                    
                    foreach (Light light in lights)
                    {
                        light.color = newColor; 
                    }

                    Debug.Log("Changed color of all lights in the group: " + groupObject.name);
                }
            }
            
            else if (specificLights.Count > 0)
            {
                foreach (Light light in specificLights)
                {
                    light.color = newColor; 
                }

                Debug.Log("Changed color of specific lights.");
            }
        }
    }
}
