using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Creatures/Hybrid", fileName = "Creature")]
public class HybridCard : CreatureCard{
    
    [Header("Hybrid Attributes")]
    [SerializeField] private CreatureType hybridType;
    
    public override double checkWeakness(ElementType attackType){
        double dmgFactor = 1;
        if (creatureType.elementalImmunities.Contains(attackType) || 
            classType.elementalImmunities.Contains(attackType) || 
            hybridType.elementalImmunities.Contains(attackType)){
            dmgFactor /= 4;
        }
    
        if (creatureType.elementalResistances.Contains(attackType) ||
            classType.elementalResistances.Contains(attackType) ||
            hybridType.elementalResistances.Contains(attackType)){
            dmgFactor /= 2;
        }
        if (creatureType.elementalWeaknesses.Contains(attackType) || 
            classType.elementalWeaknesses.Contains(attackType) || 
            hybridType.elementalWeaknesses.Contains(attackType)){
            dmgFactor *= 2;
        }
        return dmgFactor;
    }
    
    public override bool checkMyCompatibility(List<Type> checkList){
        bool result = checkList.Contains(creatureType) ||
                      checkList.Contains(hybridType) ||
                      checkList.Contains(classType);

        return result;
    }
}
