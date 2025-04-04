using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class Menu : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float delayTime = 17f;
    public GameObject mainMenu;
    public GameObject gameUI;
    private Canvas canvas;
    private Image[] imagesInCanvas;
    private Text[] textsInCanvas;
    private GameState currentState;

    // เวลาเมื่อกด ESC แล้วจะข้ามไป
    public float skipTime = 10f;

    public enum GameState
    {
        IntroPlaying,
        InMainMenu,
        InGame
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        currentState = GameState.IntroPlaying;
        LogCurrentState();

        videoPlayer.Play();

        mainMenu.SetActive(false);
        gameUI.SetActive(false);

        canvas = gameUI.GetComponent<Canvas>();
        imagesInCanvas = gameUI.GetComponentsInChildren<Image>();
        textsInCanvas = gameUI.GetComponentsInChildren<Text>();

        StartCoroutine(FadeInGameUIAfterDelay());
    }

    private IEnumerator FadeInGameUIAfterDelay()
    {
        Debug.Log("Coroutine เริ่มทำงาน");

        yield return new WaitForSeconds(delayTime);

        currentState = GameState.InMainMenu;
        LogCurrentState();

        mainMenu.SetActive(true);

        Debug.Log("แสดง Main Menu แล้ว");

        StartCoroutine(FadeInGameUI());
    }

    private IEnumerator FadeInGameUI()
    {
        foreach (var image in imagesInCanvas)
        {
            Color tempColor = image.color;
            tempColor.a = 0f;
            image.color = tempColor;
        }

        foreach (var text in textsInCanvas)
        {
            Color tempColor = text.color;
            tempColor.a = 0f;
            text.color = tempColor;
        }

        gameUI.SetActive(true);

        float fadeDuration = 3f;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);

            foreach (var image in imagesInCanvas)
            {
                Color tempColor = image.color;
                tempColor.a = alpha;
                image.color = tempColor;
            }

            foreach (var text in textsInCanvas)
            {
                Color tempColor = text.color;
                tempColor.a = alpha;
                text.color = tempColor;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        foreach (var image in imagesInCanvas)
        {
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }

        foreach (var text in textsInCanvas)
        {
            Color tempColor = text.color;
            tempColor.a = 1f;
            text.color = tempColor;
        }
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button Clicked");

        SceneManager.LoadScene("Game");
    }

    public void OnCreditButtonClicked()
    {
        Debug.Log("Credit Button Clicked");

        SceneManager.LoadScene("Credit");
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("Exit Button Clicked");
        Application.Quit();
    }

    private void LogCurrentState()
    {
        Debug.Log($"Current GameState: {currentState}");
    }

    private void Update()
    {
        // เช็คว่ากดปุ่มอะไรก็ได้ หรือคลิกเมาส์ 1 ที
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            SkipIntroAndFadeIn();
        }
    }

    private void SkipIntroAndFadeIn()
    {
        // ข้ามวิดีโอไปที่เวลาที่กำหนด
        videoPlayer.time = skipTime;

        // เริ่มกระบวนการ Fade In ทันที
        currentState = GameState.InMainMenu;
        LogCurrentState();

        // แสดง Main Menu
        mainMenu.SetActive(true);

        // เริ่ม Fade in UI ทันที
        StartCoroutine(FadeInGameUI());
    }
}
