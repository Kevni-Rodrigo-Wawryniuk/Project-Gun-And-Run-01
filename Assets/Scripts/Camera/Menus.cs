using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public static Menus menus;

    [Header("Settings")]
    public bool setting;
    [SerializeField] bool settings;
    [SerializeField] Canvas settingCanvas;

    [Header("Pause")]
    public bool pause;
    [SerializeField] bool pauser, canvasPauser;
    [SerializeField] int positionMenuPause;
    [SerializeField] GameObject[] scaleButtonPause;
    [SerializeField] Canvas pauserCanvas;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram(){
        if(menus == null){
            menus = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // pausar el juego
        Pause();
        // configuraciones del juego
        Settings();
    }

    ////////////////////////////// ACCIONES DE LOS BOTONES ///////////////////////
    public void PlayAgain(){
        pauser = !pauser;

        if(settings == false){
            canvasPauser = !canvasPauser;
        }

        if(settings == true){
            settings = false;
            canvasPauser = false;
        }
    }
    public void PlaySettings(){
        settings = !settings;
        canvasPauser = !canvasPauser;
    }
    public void OutGame(){
        // volver al menu principal
        Debug.Log("colocar la funcion de volver al menu principal");
        PlayerPrefs.SetInt("Nivel", 0);
        SceneManager.LoadScene(1);
        
    }
    //////////////////////////////////////////////////////////////////////////////

    ////////////////////////////// PAUSAR JUEGO /////////////////////////////////
    private void Pause(){
        if(pause == true){

            pauserCanvas.enabled = canvasPauser;

            if(Input.GetKeyDown(KeyCode.Escape)){
                PlayAgain();
            }

            if(pauser == true) {
                Time.timeScale = 0;
            }
            if(pauser == false ){
                Time.timeScale = 1;
                positionMenuPause = 0;
            }

            if(positionMenuPause <= -1){
                positionMenuPause = 2;
            }
            if(positionMenuPause >= 3){
                positionMenuPause = 0;
            }

            if(Input.GetKeyDown(KeyCode.W)){
                positionMenuPause--;
            }
            if(Input.GetKeyDown(KeyCode.S)){
                positionMenuPause++;
            }

            if(canvasPauser == true){

            switch(positionMenuPause){

                default:
                    scaleButtonPause[0].transform.localScale = new Vector3(1.2f,1.2f,1);
                    scaleButtonPause[1].transform.localScale = new Vector3(1,1,1);
                    scaleButtonPause[2].transform.localScale = new Vector3(1,1,1);

                    if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
                        PlayAgain();
                    }
                break;

                case 1:
                    scaleButtonPause[0].transform.localScale = new Vector3(1,1,1);
                    scaleButtonPause[1].transform.localScale = new Vector3(1.2f,1.2f,1);
                    scaleButtonPause[2].transform.localScale = new Vector3(1,1,1);
                    
                    if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
                        PlaySettings();
                    }
                break;

                case 2: 
                    scaleButtonPause[0].transform.localScale = new Vector3(1,1,1);
                    scaleButtonPause[1].transform.localScale = new Vector3(1,1,1);
                    scaleButtonPause[2].transform.localScale = new Vector3(1.2f,1.2f,1);
                    
                    if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
                        OutGame();
                    }
                break;
                
                }
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////


    ///////////////////////////// CONFIGURACION DEL JUEGO ///////////////////////
    /// VOLUMEN, BRILLO, PANTALLA
    private void Settings(){
        if(setting == true){

            settingCanvas.enabled = settings;

            if(settings == true && pauser == true){
                positionMenuPause = 1;
            }
        
        }
    }
    ////////////////////////////////////////////////////////////////////////////

}
