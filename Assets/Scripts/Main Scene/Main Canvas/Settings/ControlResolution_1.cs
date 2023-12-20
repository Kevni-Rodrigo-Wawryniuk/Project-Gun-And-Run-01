using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlResolution_1 : MonoBehaviour
{
    public static ControlResolution_1 controlResolution_1;

    [Header("Resolutions")]
    public bool resolutions;
    [SerializeField] TMP_Dropdown tMP_Dropdown;
    [SerializeField] int valueResolution;
    Resolution resolutionActual;
    
    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram(){
        if(controlResolution_1 == null){
            controlResolution_1 = this;
        }

        valueResolution = PlayerPrefs.GetInt("Resolucion", 3);

        tMP_Dropdown.value = valueResolution;

        resolutions = true;
    }

    public void ChangeResolution(int resolution){
        if(resolutions == true){
            tMP_Dropdown.value = resolution;
            valueResolution = tMP_Dropdown.value;

            PlayerPrefs.SetInt("Resolucion", valueResolution);

            if(valueResolution == 0){
                resolutionActual.width = 320;
                resolutionActual.height = 240;

                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
            if(valueResolution == 1){
                resolutionActual.width = 640;
                resolutionActual.height = 480;
            
                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
            if(valueResolution == 2){
                resolutionActual.width = 800;
                resolutionActual.height = 600;
            
                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
            if(valueResolution == 3){
                resolutionActual.width = 1080;
                resolutionActual.height = 768;
            
                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
            if(valueResolution == 4){
                resolutionActual.width = 1280;
                resolutionActual.height = 800;
            
                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
            if(valueResolution == 5){
                resolutionActual.width = 1366;
                resolutionActual.height = 768;
            
                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
            if(valueResolution == 6){
                resolutionActual.width = 1920;
                resolutionActual.height = 1200;
            
                Screen.SetResolution(resolutionActual.width, resolutionActual.height, Screen.fullScreen);
            }
        }
    }    

    public void TurnUpTheResolution(){
        if(valueResolution < 6){
            valueResolution++;
        }
        if(valueResolution > 6){
            valueResolution = 6;
        }
        
        tMP_Dropdown.value = valueResolution;
        
        PlayerPrefs.SetInt("Resolucion", valueResolution);
    }
    public void LowerResolution(){
        if(valueResolution > 0){
            valueResolution--;
        }
        if(valueResolution < 0){
            valueResolution = 0;
        }
        
        tMP_Dropdown.value = valueResolution;
        
        PlayerPrefs.SetInt("Resolucion", valueResolution);
    }
}
