using System;
using UnityEngine;
using UnityEngine.UI;
public class UI_Statbar : MonoBehaviour
{
    private Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public virtual void SetValue()
    {
        
    }

    public virtual void SetMaxValue()
    {
        
    }
}
