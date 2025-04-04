using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemPickup : MonoBehaviour
{
    public GameObject keyItem; 
    public Image itemImage;    
    public Sprite keyItemSprite;
    private bool isNearItem = false; 
    private bool isItemCollected = false;

    private void Update()
    {
        if (isNearItem && Input.GetKeyDown(KeyCode.E))
        {
            if (!isItemCollected)
            {
                CollectItem(); 
            }
            else
            {
                HideItemImage();
                keyItem.SetActive(false);
            }
        }
    }

    private void CollectItem()
    {
        isItemCollected = true;
        itemImage.sprite = keyItemSprite; 
        itemImage.enabled = true; 

        itemImage.rectTransform.anchorMin = new Vector2(0.5f, 0.5f); 
        itemImage.rectTransform.anchorMax = new Vector2(0.5f, 0.5f); 
        itemImage.rectTransform.anchoredPosition = Vector2.zero; 

        StartCoroutine(FadeInImage());
    }

    private IEnumerator FadeInImage()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;

        Color startColor = itemImage.color;
        startColor.a = 0f;
        itemImage.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            itemImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        itemImage.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
    }

    private void HideItemImage()
    {
        StartCoroutine(FadeOutImage());
    }

    private IEnumerator FadeOutImage()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;

        Color startColor = itemImage.color;
        startColor.a = 1f;
        itemImage.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); 
            itemImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        itemImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        itemImage.enabled = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isNearItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isNearItem = false;
            if (isItemCollected)
            {
                HideItemImage();
                keyItem.SetActive(false);
            }
        }
    }
}
