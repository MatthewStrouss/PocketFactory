using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoad : MonoBehaviour
{
    public Slider loadingSlider;
    public Text progressText;

    public void Start()
    {
        this.LoadGame();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadAsyncronously());
    }

    IEnumerator LoadAsyncronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            this.loadingSlider.value = progress;
            this.progressText.text = string.Format("{0}%", progress * 100f);

            yield return null;
        }
    }
}
