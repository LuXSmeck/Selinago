using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject{
    
    [Header("Effect Attributes")]
    [SerializeField] protected string effectName;
    
    public virtual void triggerEffect(){
        Debug.Log("The Effect "+ effectName +" is triggered");    
    }
}