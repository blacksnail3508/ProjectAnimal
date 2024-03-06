using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGr;
    [SerializeField] PlayableDirector director;
    [SerializeField] Slider loadingSlider;
    [SerializeField] TMP_Text progress_text;
    private void Awake()
    {
        loadingSlider.value = 0;
        loadingSlider.maxValue=100;
        progress_text.text="0%";
    }
    void Start()
    {
        // Start loading the target scene asynchronously
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSecondsRealtime(1f);
        // Create an AsyncOperation object to track the progress
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GameScene");

        // While the target scene is not yet loaded
        while (!asyncOperation.isDone)
        {
            // Update the loading progress
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f); // 0.9 is the completion value
            loadingSlider.DOKill();
            loadingSlider.DOValue(progress*100 , 0.25f);


            yield return null; // Wait until the next frame
        }

        yield return new WaitForSecondsRealtime(1f);

        //play outro
        director.Play();
        director.stopped+=delegate
        {
            gameObject.SetActive(false);
        };

    }
    public void UpdateText()
    {
        progress_text.text=Mathf.Ceil(loadingSlider.value)+"%";
    }

}
