using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

public class ControlResolution : MonoBehaviour
{
    public static ControlResolution controlResolution;

    [Header("Changer Resolution")]
    [SerializeField] int valueResolution, resolucionesValues;

    [Header("Resolutions")]
    public bool activeResolution;
    public bool useMouseChangeResolution;
    [SerializeField] TMP_Dropdown tMP_DropdownResolution;
    Resolution[] resoluciones, resolucionesNoDuplicadas;
    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram(){
        if(controlResolution == null){
            controlResolution = this;
        }
        
        activeResolution = true;
        useMouseChangeResolution = true;

        valueResolution = 0;
        valueResolution = PlayerPrefs.GetInt("ValueResolution", 0);
        
        if(activeResolution == true){
            CheckedResolutions();
        }
    }
    ///////////////////////     RESOLUCIONES    ////////////////////////////
    private void CheckedResolutions(){
      
        tMP_DropdownResolution.ClearOptions();

        resoluciones = Screen.resolutions;

        resolucionesNoDuplicadas = resoluciones.Distinct().ToArray();

        // coloco los strings de las resoluciones disponibles

        List<string> opciones = new List<string>();
        HashSet<string> opcionesDeResolucion = new HashSet<string>();

        for(int i = 0; i < resolucionesNoDuplicadas.Length; i++){

            string opcion = resolucionesNoDuplicadas[i].width + " X " + resolucionesNoDuplicadas[i].height; 
            
            if(!opcionesDeResolucion.Contains(opcion)){
                opcionesDeResolucion.Add(opcion);
                opciones.Add(opcion);   
            }
            /*
            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height){
                valueResolution = i;
            }
            */
        }
        
        resolucionesValues = resolucionesNoDuplicadas.Length;

        tMP_DropdownResolution.AddOptions(opciones);
        tMP_DropdownResolution.value = valueResolution;
        tMP_DropdownResolution.RefreshShownValue();    
    }
    
    public void ChangeResolution(int state){
        if(activeResolution == true){
           
           if(useMouseChangeResolution == true){

            Resolution resolution = resolucionesNoDuplicadas[state];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

            valueResolution = tMP_DropdownResolution.value;
        
            PlayerPrefs.SetInt("ValueResolution", valueResolution);

            Debug.Log("resolucion Actual: horizontal: " + resolution.width + " vertical: " + resolution.height);
            Debug.Log("Cambion desde el DropDown");
           }
        }
    }

    /////////////////////// CONTROL CON LAS TECLAS /////////////////////////
    
    public void ChangeResolutionForKey(){
        if(activeResolution == true){
            PlayerPrefs.SetInt("ValueResolution", valueResolution);

            Resolution resolution = resolucionesNoDuplicadas[valueResolution];  
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            tMP_DropdownResolution.value = valueResolution;
        
            Debug.Log("resolucion Actual: horizontal: " + resolution.width + " vertical: " + resolution.height);
            Debug.Log("Cambio desde las tecla");
        }
    }
    // SUBIR
    public void TurnUpTheResolution(){
        if(activeResolution == true){
            if(valueResolution < resolucionesValues){
                valueResolution++;
            }
            if(valueResolution > resolucionesValues){
                valueResolution = resolucionesValues;
            }
            
            tMP_DropdownResolution.value = valueResolution;
        }
    }

    // BAJAR
    public void LowerResolution(){
        if(activeResolution == true){
            if(valueResolution > 0){
                valueResolution--;
            }
            if(valueResolution < 0){
                valueResolution = 0;
            }
            
            tMP_DropdownResolution.value = valueResolution;
        }
    }
    ////////////////////////////////////////////////////////////////////////
}
