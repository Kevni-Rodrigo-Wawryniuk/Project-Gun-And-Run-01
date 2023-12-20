using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class Shine : MonoBehaviour
{
    public static Shine shine;

    [Header(" Shine ")]
    public bool shines;
    [SerializeField] float shineValue;
    [SerializeField] Slider sliderShine;
    [SerializeField] Image imageShine;

    // Start is called before the first frame update
    void Start()
    {
        StartProgram();
    }

    private void StartProgram(){
        if(shine == null){
            shine = this;
        }

        shines = true;

        if(shines == true){
            sliderShine.maxValue = 0.8f;
            sliderShine.minValue = 0;

            sliderShine.value = PlayerPrefs.GetFloat("shinevalue", 0.3f);
            imageShine.color = new Color(255,255,255,sliderShine.value);
        }
    }

    ///////////////////////// CAMBIAR EL BRILLO /////////////////////
    public void ChangerShine(float value){
        if(shines == true){
            shineValue = value;
            PlayerPrefs.SetFloat("shinevalue", shineValue);
            imageShine.color = new Color(255,255,255,sliderShine.value);
        }  
    }
    //  CAMBIO DEL VALOR POR MEDIO DE LAS TECLAS

    // SUBIR
    public void TurnUpTheShine(){
        if(shine == true){
            if(shineValue <= 1){
                shineValue += 0.1f;
            }
            if(shineValue > 1){
                shineValue = 1;
            }
            PlayerPrefs.SetFloat("shinevalue", shineValue);
            sliderShine.value = shineValue;
            
            imageShine.color = new Color(255,255,255,sliderShine.value);
        }   
    }
    // BAJAR
    public void LowerShine(){
        if(shines == true){
            if(shineValue >= 0){
                shineValue -= 0.1f;
            }
            if(shineValue < 0){
                shineValue = 0;
            }
            PlayerPrefs.SetFloat("shinevalue", shineValue);
            sliderShine.value = shineValue;
            imageShine.color = new Color(255,255,255,sliderShine.value);
        }
    }
    ////////////////////////////////////////////////////////////////
}