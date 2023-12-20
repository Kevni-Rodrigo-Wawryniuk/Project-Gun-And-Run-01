using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MainMenus : MonoBehaviour
{
    public static MainMenus mainMenus;

    [Header("Change Settings")]
    [SerializeField] Volume volume;
    [SerializeField] Shine shine;
    [SerializeField] FullScreem fullScreem;
    [SerializeField] ControlResolution_1 controlResolution_1;
    [SerializeField] ControlQuality controlQuality;

    [Header("Main Music")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource efectButtonSelect;
    [SerializeField] AudioSource effectButtonEnter;

    [Header("Main Menu")]
    public bool mainMenu;
    [SerializeField] bool mainCanvas;
    [SerializeField] Canvas canvasMain;
    [SerializeField] int selectionButtonMainMenu;
    [SerializeField] RectTransform[] sizeButtonMainMenu;

    [Header("Setting Menu")]
    public bool settingMenu;
    [SerializeField] bool settingCanvas;
    [SerializeField] Canvas canvasSettings;
    [SerializeField] int selectionButtonSettingMenu;
    [SerializeField] RectTransform[] sizeButtonSettingMenu;
    [SerializeField] GameObject[] buttonSettingActive;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram(){
        // esto es para los cambios de escena o si el objeto no esta en escena
        if(mainMenus == null){
            mainMenus = this;
        }
        // variables que controlan si se pueden acceder a los menus
        mainMenu = true;
        settingMenu = true;

        // variable de control de la musica del manu principal
        music.Play(); 

        // las pantallas activas al iniciar la escena
        mainCanvas = true;
        settingCanvas = false;
        
        // boton seleccionado al iniciar el juego
        selectionButtonMainMenu = 0;
        // boton seleccionado en el menu de configuraciones
        selectionButtonSettingMenu = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // Controla el menu principal
        MainMenu();   
        // Controla el menu de pociones
        SettingMenu();
    }

    ///////////     MENUS     ////////////
    private void ControlMenusActive(){
        if(settingCanvas == true){
            selectionButtonMainMenu = 1;
        }
    }
    private void MainMenu(){
        if(mainMenu == true){

            canvasMain.enabled = mainCanvas;

            if(mainCanvas == true){
                if(selectionButtonMainMenu < 0){
                    selectionButtonMainMenu = 2;
                }
                if(selectionButtonMainMenu > 2){
                    selectionButtonMainMenu = 0;
                }

                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
                    selectionButtonMainMenu--;
                    efectButtonSelect.Play();
                }
                if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
                    selectionButtonMainMenu++;
                    efectButtonSelect.Play();
                }

                switch(selectionButtonMainMenu){
                    // boton de inicio del juego
                    default:
                        sizeButtonMainMenu[0].localScale = new Vector3(1.2f,1.2f,1);
                        sizeButtonMainMenu[1].localScale = new Vector3(1,1,1);
                        sizeButtonMainMenu[2].localScale = new Vector3(1,1,1);

                        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                            ButtonPlayGame();
                        }
                    break;
                    // boton de las configuraciones del juego 
                    case 1:
                        sizeButtonMainMenu[0].localScale = new Vector3(1,1,1);
                        sizeButtonMainMenu[1].localScale = new Vector3(1.2f,1.2f,1);
                        sizeButtonMainMenu[2].localScale = new Vector3(1,1,1);

                        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                            ButtonSettings();
                        }
                    break;
                    // botton de salida del juego
                    case 2:
                        sizeButtonMainMenu[0].localScale = new Vector3(1,1,1);
                        sizeButtonMainMenu[1].localScale = new Vector3(1,1,1);
                        sizeButtonMainMenu[2].localScale = new Vector3(1.2f,1.2f,1);

                        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                            ButtonQuitGame();
                        }
                    break;
                }
            }
        }
    }
    private void SettingMenu(){
        if(settingMenu == true){

            canvasSettings.enabled = settingCanvas;

            if(settingCanvas == true){
                if(selectionButtonSettingMenu < 0){
                    selectionButtonSettingMenu = 5;
                }
                if(selectionButtonSettingMenu > 5){
                    selectionButtonSettingMenu = 0;
                }

                if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
                    selectionButtonSettingMenu--;
                    efectButtonSelect.Play();
                }
                if(Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow)){
                    selectionButtonSettingMenu++;
                    efectButtonSelect.Play();
                }

                switch(selectionButtonSettingMenu){
                    default:
                       buttonSettingActive[0].SetActive(true);
                       buttonSettingActive[1].SetActive(false);
                       buttonSettingActive[2].SetActive(false);
                       buttonSettingActive[3].SetActive(false);
                       buttonSettingActive[4].SetActive(false);

                       sizeButtonSettingMenu[0].localScale = new Vector3(1.2f,1.2f,1);
                       sizeButtonSettingMenu[1].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[2].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[3].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[4].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[5].localScale = new Vector3(1,1,1);

                       if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                        volume.TurnUpTheVolume();
                       }

                       if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                        volume.LowerVolume();
                       }
                    break;

                    case 1:
                       buttonSettingActive[0].SetActive(false);
                       buttonSettingActive[1].SetActive(true);
                       buttonSettingActive[2].SetActive(false);
                       buttonSettingActive[3].SetActive(false);
                       buttonSettingActive[4].SetActive(false);

                       sizeButtonSettingMenu[0].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[1].localScale = new Vector3(1.2f,1.2f,1);
                       sizeButtonSettingMenu[2].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[3].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[4].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[5].localScale = new Vector3(1,1,1);
                       
                       if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                        shine.TurnUpTheShine();
                       }

                       if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                        shine.LowerShine();
                       }
                    break;

                    case 2:
                       buttonSettingActive[0].SetActive(false);
                       buttonSettingActive[1].SetActive(false);
                       buttonSettingActive[2].SetActive(true);
                       buttonSettingActive[3].SetActive(false);
                       buttonSettingActive[4].SetActive(false);

                       sizeButtonSettingMenu[0].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[1].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[2].localScale = new Vector3(1.2f,1.2f,1);
                       sizeButtonSettingMenu[3].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[4].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[5].localScale = new Vector3(1,1,1);
                       
                       if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                        fullScreem.changeScreen();
                       }

                    break;

                    case 3:
                       buttonSettingActive[0].SetActive(false);
                       buttonSettingActive[1].SetActive(false);
                       buttonSettingActive[2].SetActive(false);
                       buttonSettingActive[3].SetActive(true);
                       buttonSettingActive[4].SetActive(false);

                       sizeButtonSettingMenu[0].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[1].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[2].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[3].localScale = new Vector3(1.2f,1.2f,1);
                       sizeButtonSettingMenu[4].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[5].localScale = new Vector3(1,1,1);

                       
                        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                            controlResolution_1.TurnUpTheResolution();
                        }

                        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                            controlResolution_1.LowerResolution();
                        }
                        
                    break;

                    case 4:
                       buttonSettingActive[0].SetActive(false);
                       buttonSettingActive[1].SetActive(false);
                       buttonSettingActive[2].SetActive(false);
                       buttonSettingActive[3].SetActive(false);
                       buttonSettingActive[4].SetActive(true);

                       sizeButtonSettingMenu[0].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[1].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[2].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[3].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[4].localScale = new Vector3(1.2f,1.2f,1);
                       sizeButtonSettingMenu[5].localScale = new Vector3(1,1,1);
                       
                       if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                        controlQuality.TurnUpTheQuality();
                       }

                       if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                        controlQuality.LowerQuality();
                       }
                    break;

                    case 5:
                       buttonSettingActive[0].SetActive(false);
                       buttonSettingActive[1].SetActive(false);
                       buttonSettingActive[2].SetActive(false);
                       buttonSettingActive[3].SetActive(false);
                       buttonSettingActive[4].SetActive(false);

                       sizeButtonSettingMenu[0].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[1].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[2].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[3].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[4].localScale = new Vector3(1,1,1);
                       sizeButtonSettingMenu[5].localScale = new Vector3(1.2f,1.2f,1);

                       if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
                        ButtonSettings();
                       }
                    break;
                }
            }
        }
    }
    ///////////////////////////////////////////////

    //////////      FUNCIONES DE LOS BOTTONES    ///////////
    public void ButtonPlayGame(){
        // iniciar el juego
        Debug.Log("Iniciar el juego");
        effectButtonEnter.Play();
    }
    public void ButtonSettings(){
        // entrar al menu de opciones
        effectButtonEnter.Play();
        if(mainCanvas == true){
            Debug.Log("Configuraciones");
            selectionButtonSettingMenu = 0;
        }
        if(settingCanvas == true){
            Debug.Log("Pantalla Principal");
            selectionButtonMainMenu = 1;
        }
        mainCanvas = !mainCanvas;
        settingCanvas = !settingCanvas;
    }
    public void ButtonQuitGame(){
        // salir del juego
        effectButtonEnter.Play();
        Debug.Log("Saliste del Juego");
        Application.Quit();
    }
    ////////////////////////////////////////////////////////
}
