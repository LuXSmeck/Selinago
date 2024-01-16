using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effect/UpgradeEffect/ConstructionBoostEffect", fileName = "Effect")]
public class ConstructionBoostEffect : AUpgradeEffect{

   [SerializeField] private int atkBoost;
   [SerializeField] private int defBoost;
   [SerializeField] private AAttackEffect bonusEffect;

   
   public ConstructionBoostEffect(){
      description = "Boostes the stats of spawning Creatures";
   }

   public void apply(Creature creature){
      creature.increaseStrength(atkBoost);
      creature.increaseArmor(defBoost);
      if (bonusEffect != null){
         creature.addAttackEffeckt(bonusEffect);
      }
   }
}
