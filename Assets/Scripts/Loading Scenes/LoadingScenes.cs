using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScenes : MonoBehaviour
{
    public static LoadingScenes loadingScenes;

    [Header("Load scene")]
    public bool loadScene;
    public int niveles;
    [SerializeField] Slider sliderLoading;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram(){
        if(loadingScenes == null){
            loadingScenes = this;
        }
        loadScene = true;

        sliderLoading.maxValue = 0.9f;
        sliderLoading.minValue = 0;

        niveles = PlayerPrefs.GetInt("Nivel", 0);
        
        if(loadScene == true){
            StartCoroutine(LoadingScene(niveles));
        }
    }

    IEnumerator LoadingScene(int nivel){

        AsyncOperation operation = SceneManager.LoadSceneAsync(nivel);
        operation.allowSceneActivation = false;

        while(!operation.isDone){

            sliderLoading.value = operation.progress;

            if(operation.progress >= 0.9f){
                if(Input.anyKey){
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
