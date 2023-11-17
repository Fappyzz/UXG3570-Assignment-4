using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChange : MonoBehaviour
{
    #region Slider GameObject + Text 
    public Slider slider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public TextMeshProUGUI sliderText;
    #endregion

    #region Minimum Font Size
    public int fontSizeValue = 0;
    #endregion

    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }


    // Update is called once per frame
    void Update()
    {
        //Increase slider text with the value of the sound for feedback
        sliderText.text = slider.value.ToString() + "%";
        //Increase slider text with the value of the sound for feedback
        sliderText.fontSize = slider.value + fontSizeValue;

        //if the font size goes above 100, cap it at 100
        if (sliderText.fontSize > 100)
        {
            sliderText.fontSize = 100;
        }

        //if the font size goes below 10, cap it at 60
        if (sliderText.fontSize < 60)
        {
            sliderText.fontSize = 60;
        }
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
    }

    public void OnDragDelegate(PointerEventData data)
    {
        AudioManager.instance.PlaySFX("Sliders");
        Debug.Log("moving");
    }
}
