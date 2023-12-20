using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public static Volume volume;

    [Header("Global Volume")]
    public bool volumes;
    [SerializeField] float VolumeValue;
    [SerializeField] Slider sliderVolume;
    [SerializeField] Image imageVolumeOff;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram(){
        if(volume == null){
            volume = this;
        }

        volumes = true;

        if(volumes == true){
            sliderVolume.maxValue = 1;
            sliderVolume.minValue = 0;

            sliderVolume.value = PlayerPrefs.GetFloat("volumevalue", 1);
            AudioListener.volume = sliderVolume.value;
            CheckVolume();
        }
    }
 
    //////////////////////////////////// CAMBIAR VOLUMEN //////////////////////////////////
    public void ChangerVolume(float value){
        if(volumes == true){
            VolumeValue = value;
            PlayerPrefs.SetFloat("volumevalue", VolumeValue);
            AudioListener.volume = sliderVolume.value;
            CheckVolume();
        }
    }
   
    // UTILIZANDO LAS TECLAS PARA MODIFICAR EL VOLUMEN
    
    // SUBIR VOLUMEN 
    public void TurnUpTheVolume(){
        if(volumes == true){
            if(VolumeValue <= 1){
                VolumeValue += 0.1f;
            }
            if(VolumeValue > 1){
                VolumeValue = 1;
            }
            PlayerPrefs.SetFloat("volumevalue", VolumeValue);
            sliderVolume.value = VolumeValue;
            AudioListener.volume = sliderVolume.value;
            CheckVolume();
        }
    }

    // BAJAR VOLUMEN 
    public void LowerVolume(){
        if(volumes == true){
            if(VolumeValue >= 0){
                VolumeValue -= 0.1f;
            }
            if(VolumeValue < 0){
                VolumeValue = 0;
            }

            PlayerPrefs.SetFloat("volumevalue", VolumeValue);
            sliderVolume.value = VolumeValue;
            AudioListener.volume = sliderVolume.value;
            CheckVolume();
        }
    }
    /////////////////////// REVISAR SI EL VOLUMEN ESTA ACTIVO /////////////////////////////
    public void CheckVolume(){
        if(volumes == true){
            if(sliderVolume.value == 0){
                imageVolumeOff.enabled = true;
            }else{
                imageVolumeOff.enabled = false;
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////
}
