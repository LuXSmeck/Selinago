using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Equipable/Weapon", fileName = "Weapon")]

public class WeaponCard : EquipableCard{
    
    [Header("Weapon Attributes")]
    [SerializeField] private WeaponType type;
    [SerializeField] private int attackBoost;
    [SerializeField] private int defenceBoost;
    [SerializeField] private AAttackEffect attackEffect;
    
    /// <summary> Checks if a Monster can be equiped with this Weapon </summary>
    /// <param name="cardSlot"></param>
    /// <returns> TRUE if the equipment could be resolved </returns>
    public override bool equipCard(CardSlot cardSlot){
        bool result = false;
        if (cardSlot.cardReference != null && cardSlot.cardReference is CreatureCard){
            if (checkCompatibility((CreatureCard)cardSlot.cardReference)){
                addEffects(cardSlot);
                result = true;
            }
        }

        return result;
    }
    
    private void addEffects(CardSlot cardSlot){
        Creature creature = cardSlot.fieldReference.getCreature();
        creature.increaseStrength(attackBoost);
        creature.increaseArmor(defenceBoost);
        if (attackEffect != null){
            creature.addAttackEffeckt(attackEffect);
        }
    }

    public override void removeEffects(CardSlot cardSlot){
        Creature creature = cardSlot.fieldReference.getCreature();
        creature.increaseStrength(-attackBoost);
        creature.increaseArmor(-defenceBoost);
        if (attackEffect != null){
            creature.removeAttackEffeckt(attackEffect);
        }
    }


    //************************************************************************************************* Getter & Setters
    private bool checkCompatibility(CreatureCard creatureCard){
        bool resultWhite = true, resultBlack = true;
        
        if (type.CreatureWhitelist.Count > 0){
            resultWhite = creatureCard.checkMyCompatibility(type.CreatureWhitelist);
        }
        if (type.CreatureBlacklist.Count > 0){
            resultBlack = ! creatureCard.checkMyCompatibility(type.CreatureBlacklist);
        }
        
        return (resultWhite && resultBlack);
    }
}
