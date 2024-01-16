using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Equipable/Upgrade", fileName = "Equipment")]

public class UpgradeCard : EquipableCard{
    
    [Header("Upgrade Attributes")]
    [SerializeField] private int durabilityBoost;
    [SerializeField] private AUpgradeEffect upgradeEffect;
    
    public override bool equipCard(CardSlot cardSlot){
        bool result = false;
        if (cardSlot.cardReference != null && cardSlot.cardReference is BuildingCard){
            addEffects(cardSlot);
            result = true;
        }

        return result;
    }

    protected void addEffects(CardSlot cardSlot){
        Building building = cardSlot.fieldReference.getBuilding();
        building.increaseDurability(durabilityBoost);
        if (upgradeEffect != null){
            building.addUpgradeEffeckt(upgradeEffect);
        }
    }
    
    public override void removeEffects(CardSlot cardSlot){
        Building building = cardSlot.fieldReference.getBuilding();
        building.takeDamage(durabilityBoost);
        if (upgradeEffect != null){
            building.removeUpgradeEffeckt(upgradeEffect);
        }
    }
    
    //************************************************************************************************* Getter & Setters

}
