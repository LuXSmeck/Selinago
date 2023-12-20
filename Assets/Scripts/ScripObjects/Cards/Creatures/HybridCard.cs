using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Creatures/Hybrid", fileName = "Creature")]
public class HybridCard : CreatureCard{
    
    [Header("Hybrid Attributes")]
    [SerializeField] private CreatureType hybridType;
    
    override 
    public double checkWeakness(ElementType pAttackType){
        double dmgFactor = 1;
        if (creatureType.elementalImmunities.Contains(pAttackType) || 
            classType.elementalImmunities.Contains(pAttackType) || 
            hybridType.elementalImmunities.Contains(pAttackType)){
            dmgFactor /= 4;
        }
    
        if (creatureType.elementalResistances.Contains(pAttackType) ||
            classType.elementalResistances.Contains(pAttackType) ||
            hybridType.elementalResistances.Contains(pAttackType)){
            dmgFactor /= 2;
        }
        if (creatureType.elementalWeaknesses.Contains(pAttackType) || 
            classType.elementalWeaknesses.Contains(pAttackType) || 
            hybridType.elementalWeaknesses.Contains(pAttackType)){
            dmgFactor *= 2;
        }
        return dmgFactor;
    }
    
}
