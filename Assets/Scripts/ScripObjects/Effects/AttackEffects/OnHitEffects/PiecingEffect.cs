using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AttackEffect/PiecingEffect", fileName = "Effect")]
public class PiecingEffect : AOnHitEffect{

   public PiecingEffect(){
      priority    = 1;
      description = "Ignores the Armor of the target and does full dmg to its strenght." +
                    "This damage can't do overdamage!";
   }

   /// <summary> This Method is called by the Attackers "attack" Method BEFORE the defenders "takeDamage" is called.
   /// It does NOT replace that Method but if necessary it calls itself the original one and ends the sequence.
   /// Depending on the returnvalue, the Attacksequence is ended an no other effects should be executed. </summary>
   /// <param name="incommingDamage"></param>
   /// <param name="attacker"></param>
   /// <param name="defender"></param>
   /// <returns> TRUE if the Attacksequence can continue as normal. FALSE if the Effect ended the Attacksequence. </returns>
   public override bool modifiedTakeDamage(double incommingDamage, Creature attacker, Creature defender){
      if (defender.Strength == 0){
         return true;
      } else{
         if (defender.Strength < (int)(incommingDamage)){
            incommingDamage = defender.Strength;
         }
         defender.damageStrength((int)incommingDamage);
         Debug.Log(" The Attack ignored the Armor of " + defender.getReference().getName() + " for "+ incommingDamage +" Damage.");
         return false;
      }
   }
}
