using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMainScene_1 : MonoBehaviour
{
    public static GameManagerMainScene_1 gameManagerMainScene_1;

    [Header("Objects in Start Scene")]
    public bool objectsStarts;
    [SerializeField] float timeLoad;
    [SerializeField] SpriteRenderer stardustTitelSprite;
    [SerializeField] float tranparentSpriteTitel;
    [SerializeField] float timeTitel, endTimeTitel;
    [SerializeField] Image courtine;
    [SerializeField] float colorTransparent;
    [SerializeField] float timeCourtine, endTiemCourtien;
    [SerializeField] Transform planet, backGround;
    [SerializeField] float speedPlanet, speedBackGround;
    [SerializeField] MeshRenderer stars;
    [SerializeField] float speedStarsx, speedStarsy;

    [Header("ChangerScene")]
    public bool changerSceneActive;
    [SerializeField] float timePressAnyButton, endTimePressAnyButton, textPressButtonTransparent;
    [SerializeField] TMP_Text textPressButton, textTitel;
    
    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram(){
        if(gameManagerMainScene_1 == null){
            gameManagerMainScene_1 = this;
        }
        Time.timeScale = 1;
        changerSceneActive = true;
        objectsStarts = true;

        stardustTitelSprite.color = new Color(0,0,0,0);
        
        textPressButton.color = new Color(0,0,0,0);
    }
    // Update is called once per frame
    void Update()
    {
        ObjectsinStart();
        ChangerScene();
    }
    private void ChangerScene(){
        if(changerSceneActive == true){

            if(timeCourtine < endTiemCourtien){
                timeCourtine += timeLoad * Time.deltaTime;
            }

            if(timeCourtine >= endTiemCourtien){
                
                courtine.color = courtine.color -= new Color(0,0,0,colorTransparent) * Time.deltaTime;

                if(timeTitel < endTimeTitel){
                    timeTitel += timeLoad * Time.deltaTime;
                }
            }

            if(timeTitel >= endTimeTitel){
                    
                stardustTitelSprite.color += new Color(255,255,255,tranparentSpriteTitel) * Time.deltaTime;

                if(timePressAnyButton < endTimePressAnyButton){
                   timePressAnyButton += timeLoad * Time.deltaTime;
                }
            }

            if(timePressAnyButton >= endTimePressAnyButton){

               textPressButton.color += new Color(255,255,255,textPressButtonTransparent) * Time.deltaTime;

                if(Input.anyKeyDown){
                    PlayerPrefs.SetInt("Nivel", 2);
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
    private void ObjectsinStart(){
        if(objectsStarts == true){

            planet.localEulerAngles += new Vector3(0,0,speedPlanet);

            backGround.localEulerAngles += new Vector3(0,0,speedBackGround);

            stars.material.mainTextureOffset = stars.material.mainTextureOffset += new Vector2(speedStarsx, speedStarsy) * Time.deltaTime;

        }
    }
}
