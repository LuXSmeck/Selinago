using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AttackEffect", fileName = "Effect")]
public class AttackEffect : Effect{

   [SerializeField] private AttackEffectTypeEnum type;
   [SerializeField] private int value;

   public AttackEffectTypeEnum Type => type;
   public int Value => value;

   public void onHit(){
      
   }
  
}
