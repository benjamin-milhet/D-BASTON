using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AttackPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI valueSlider;
    public Slider nbDe;

    public void SetValueTextSlider()
    {
        valueSlider.text = nbDe.value.ToString();
    }
    
    

}
