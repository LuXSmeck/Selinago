using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AttackEffect/DeadlyEffect", fileName = "Effect")]
public class DeadlyEffect : AOnHitEffect{

   public DeadlyEffect(){
      priority = 5;
      description = "If the defender takes dmg from this attack, he will instantly die.";
   }

   /// <summary> This Method is called by the Attackers "attack" Method BEFORE the defenders "takeDamage" is called.
   /// It does NOT replace that Method but if necessary it calls itself the original one and ends the sequence.
   /// Depending on the returnvalue, the Attacksequence is ended an no other effects should be executed. </summary>
   /// <param name="incommingDamage"></param>
   /// <param name="attacker"></param>
   /// <param name="defender"></param>
   /// <returns> TRUE if the Attacksequence can continue as normal. FALSE if the Effect ended the Attacksequence. </returns>
   public override bool modifiedTakeDamage(double incommingDamage, Creature attacker, Creature defender){
      if (incommingDamage > defender.Armor){
         defender.die();
         return false;
      } else{
         return true;
      }
   }
}
