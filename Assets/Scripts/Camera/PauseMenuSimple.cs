using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuSimple : MonoBehaviour
{
    public static PauseMenuSimple pauseMenuSimple;

    [Header("Pausar El Juego")]
    public bool pause;
    [SerializeField] bool pauser;
    [SerializeField] Canvas canvasPause;

    [Header("Control Con teclas")]
    public bool keyControl;
    [SerializeField] bool keyControlActive;
    [SerializeField] int selectionButton;
    [SerializeField] RectTransform[] buttonSelection;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram(){
        if(pauseMenuSimple == null){
            pauseMenuSimple = this;
        }

        selectionButton = 0;
        
        pause = true;
        keyControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        PauserGame();
        MenuControl();
    }

    // PAUSAR EL JUEGO // 
    private void PauserGame(){
        if(pause == true){

            canvasPause.enabled = pauser;

            if(Input.GetKeyDown(KeyCode.Escape)){
                ButtonReturn();
            }

            if(pauser == true){
                Time.timeScale = 0;
            }else{
                Time.timeScale = 1;
            }
        }
    }
    //                 //
    // CONTROL DE MENU //
    private void MenuControl(){
        if(keyControl == true){

            if(keyControlActive == true){

                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
                    selectionButton--;
                }

                if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
                    selectionButton++;
                }

                if(selectionButton < 0){
                    selectionButton = 1;
                }
                if(selectionButton > 1){
                    selectionButton = 0;
                }
                
                switch(selectionButton){
                    default:
                        buttonSelection[0].transform.localScale = new Vector3(1.2f,1.2f,1);
                        buttonSelection[1].transform.localScale = new Vector3(1,1,1);

                        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                            ButtonReturn();
                        }
                    
                    break;
                    
                    case 1:
                        buttonSelection[0].transform.localScale = new Vector3(1,1,1);
                        buttonSelection[1].transform.localScale = new Vector3(1.2f,1.2f,1);

                        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                            ButtonQuit();
                        }
                    
                    break;
                }
            }

        }
    }
    //                 //
    // FUNCIONES DE LOS BOTONES //
    public void ButtonReturn(){
        if(pause == true){
            selectionButton = 0;
            pauser = !pauser;
            keyControlActive = !keyControlActive;
        }
    }
    public void ButtonQuit(){
        if(pause == true){
            PlayerPrefs.SetInt("Nivel", 0);
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
    //                          //

}
