using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Weapon", fileName = "Weapon")]

public class WeaponCard : EquipableCard{
    
    [Header("Weapon Attributes")]
    [SerializeField] private WeaponType type;
    [SerializeField] private int attackBoost;
    [SerializeField] private int defenceBoost;
    [SerializeField] private AAttackEffect attackEffect;
    
    public override bool equipCard(CardSlot cardSlot){
        bool result = false;
        if (cardSlot.cardReference != null && cardSlot.cardReference is CreatureCard){
            if (checkCompatibility((CreatureCard)cardSlot.cardReference)){
                cardSlot.addEquipment(this);
                addEffects(cardSlot);
                result = true;
            }
        }

        return result;
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
    private void addEffects(CardSlot cardSlot){
        Creature creature = cardSlot.fieldReference.getCreature();
        creature.increaseStrength(attackBoost);
        creature.increaseArmor(defenceBoost);
        if (attackEffect != null){
            creature.addAttackEffeckt(attackEffect);
        }
    }
    
    private bool checkCompatibility(CreatureCard creatureCard){
        bool result;
        if (type.CreatureWhitelist.Count > 0){
            result = creatureCard.checkMyCompatibility(type.CreatureWhitelist);
        } else{
            result = ! creatureCard.checkMyCompatibility(type.CreatureBlacklist);
        }
        return result;
    }
}
