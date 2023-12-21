using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AttackEffect/RangeEffect", fileName = "Effect")]
public class RangeEffect : AAttackEffect{

   [SerializeField] private int value;
   public int Value => value;

   public RangeEffect(){
      type = AttackEffectTypeEnum.RANGE;
      description = "Increased the range of an Attack about the given value. " +
                    "NOT cumulative!";
   }
}
