using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;
using Object = UnityEngine.Object;

public class BossCreature : Creature{

    [SerializeField] private int life;
    
    public void initialize(CardSlot cardSlot){
        base.initialize(cardSlot);
        life     = cardReference.Hp;
    }

    //************************************************************** Capabilitie Methods
    
    //************************************************************** Fighting Methods
    public void takeDamage(double incommingDamage, Creature attacker){
        if (incommingDamage >= armor){
            double finalDamage = incommingDamage - armor;
            life -= (int) Math.Ceiling(incommingDamage/2);
            life -= (int) finalDamage;
        }else{
            life -= (int) Math.Ceiling(incommingDamage/2);
        }
 
        checkLife();
    }
    
    //************************************************************** private Methods
    private void checkLife(){
        if (life <= 0){
            state = CreatureStateEnum.DEAD;
            Debug.Log(cardReference.getName() +" failted");
        }
    }

    //************************************************************************************************* Getter & Setters
    
}
