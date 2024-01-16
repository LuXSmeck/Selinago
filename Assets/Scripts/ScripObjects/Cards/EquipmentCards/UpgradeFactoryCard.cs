using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Equipable/UpgradeFactory", fileName = "Equipment")]

public class UpgradeFactoryCard : UpgradeCard{
    
    public override bool equipCard(CardSlot cardSlot){
        bool result = false;
        if (cardSlot.cardReference != null && cardSlot.cardReference is FactoryCard){
            addEffects(cardSlot);
            result = true;
        }

        return result;
    }
    
    //************************************************************************************************* Getter & Setters

}
