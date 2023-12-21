using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAfterHitEffect : AAttackEffect{

   public AAfterHitEffect(){
      type = AttackEffectTypeEnum.AFTERHIT;
   }

   /// <summary> This Effect will trigger after a given creature got Hit </summary>
   /// <param name="defender"></param>
   public abstract void performEffect(Creature defender);

}
