using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlQuality : MonoBehaviour
{
    public static ControlQuality controlQuality;

    [Header("Quality")]
    public bool quality;
    [SerializeField] TMP_Dropdown tMP_DropdownQuality;
    [SerializeField] int stateQuality;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }
    private void StartProgram(){
        if(controlQuality == null){
            controlQuality = this;
        }
        quality = true;

        if(quality == true){
            stateQuality = PlayerPrefs.GetInt("StateQuality", 3);
            tMP_DropdownQuality.value = stateQuality;
            ChangeQuality();
        }
    }
    
    public void ChangeQuality(){
        if(quality == true){
            stateQuality = tMP_DropdownQuality.value;
            QualitySettings.SetQualityLevel(tMP_DropdownQuality.value);
            PlayerPrefs.SetInt("StateQuality", tMP_DropdownQuality.value);
            Debug.Log("Calidad: " + tMP_DropdownQuality.value);
        }
    }

    ////////////////// CONTROL CON TECLAS //////////////////
    // SUBIR
    public void TurnUpTheQuality(){
        if(quality == true){
            if(stateQuality < 6){
                stateQuality++;
            }
            if(stateQuality > 6){
                stateQuality = 6;
            }

            tMP_DropdownQuality.value = stateQuality;
            QualitySettings.SetQualityLevel(tMP_DropdownQuality.value);
            PlayerPrefs.SetInt("StateQuality", tMP_DropdownQuality.value);

            Debug.Log("Calidad: " + tMP_DropdownQuality.value);
        }
    }
    //BAJAR
    public void LowerQuality(){
        if(quality == true){
            if(stateQuality > 0){
                stateQuality--;
            }
            if(stateQuality < 0){
                stateQuality = 0;
            }
            
            tMP_DropdownQuality.value = stateQuality;
            QualitySettings.SetQualityLevel(tMP_DropdownQuality.value);
            PlayerPrefs.SetInt("StateQuality", tMP_DropdownQuality.value);
            
            Debug.Log("Calidad: " + tMP_DropdownQuality.value);
        }   
    }
    ////////////////////////////////////////////////////////
}
