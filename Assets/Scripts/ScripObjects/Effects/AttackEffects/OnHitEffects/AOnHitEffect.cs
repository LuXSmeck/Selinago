using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AOnHitEffect : AAttackEffect{

   public AOnHitEffect(){
      type = AttackEffectTypeEnum.ONHIT;
   }

   /// <summary> This Method is called by the Attackers "attack" Method BEFORE the defenders "takeDamage" is called.
   /// It does replace that Method and if necessary calls itself the original one. </summary>
   /// <param name="incommingDamage"></param>
   /// <param name="attacker"></param>
   /// <param name="defender"></param>
   public abstract bool modifiedTakeDamage(double incommingDamage, Creature attacker, Creature defender);

}
