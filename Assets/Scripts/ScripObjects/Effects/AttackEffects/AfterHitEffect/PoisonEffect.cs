using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/AttackEffect/PoisonEffect", fileName = "Effect")]
public class PoisonEffect : AAfterHitEffect{

   public PoisonEffect(){
      description = "Poisons a hit enemy.";
   }
   
   public override void performEffect(Creature defender){
      throw new NotImplementedException();
   }
}
