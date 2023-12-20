using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableCard : Card{
    
 //   [Header("Equip Attributes")]
    
/* TODO -> CardPlacingManager
    public virtual void tryToEquipCard(Card pTargetCard){
        bool result = false;
        switch (UpgradableCardType){
            case CardTypeEnum.Area :
                if (pTargetCard is FieldCard){ 
                    FieldCard targetFieldCard = (FieldCard)pTargetCard;
                    if (targetFieldCard.getFieldType() == "Area"){
                        result = false;
                    }
                }
                break;
            case CardTypeEnum.Building : break;
            case CardTypeEnum.Creature : break;
        }
        if (pTargetCard is CreatureCard){
            
        }
    }
*/
    protected virtual void equipCard(){
        Debug.Log("Card "+ cardName +" got equiped.");
    }
}
