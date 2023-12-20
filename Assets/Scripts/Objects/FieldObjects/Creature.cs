using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;
using Object = UnityEngine.Object;

public class Creature : AFieldObject{

    [Header("Card Attributes")]
    [SerializeField] private CreatureCard cardReference;
    [SerializeField] private CardSlot cardSlot;
    
    [Header("Creature Attributes")]
    [SerializeField] private int life;
    [SerializeField] private int strength;
    [SerializeField] private int armor;
    [SerializeField] private int movement;
    [SerializeField] private CreatureStateEnum state;
    
    [Header("Creature Effects")]
    [SerializeField] private List<AttackEffect> attackEffects;
    
    public void initialize(CardSlot cardSlot){
        this.cardSlot = cardSlot;
        cardReference = (CreatureCard)cardSlot.cardReference;

        life     = cardReference.Hp;
        strength = cardReference.Atk;
        armor    = cardReference.Def;
        
        movement = cardReference.getLevel();
        if (movement > 8){
            movement = 8;
        }else if (movement < 1){
            movement = 1;
        }
        
        state = CreatureStateEnum.EXHAUSTED;
    }

    public void destroy(){
        Destroy(this);
    }

    //************************************************************** Capabilitie Methods
    public void endTurn(){
        if (state == CreatureStateEnum.STUNNED){
            state = CreatureStateEnum.EXHAUSTED;
        }else if (state == CreatureStateEnum.EXHAUSTED){
            state = CreatureStateEnum.READY;
        }
    }
    
    public void increaseStun(){  //TODO better name
        if (state == CreatureStateEnum.EXHAUSTED){
            state = CreatureStateEnum.STUNNED;
        }else if (state == CreatureStateEnum.READY){
            state = CreatureStateEnum.EXHAUSTED;
        }
    }
    
    /// <summary> Removes itself from the active Field and places itself on the target location.
    /// There are no validation-checks at all! </summary>
    /// <param name="targetField"></param>
    public void move(Field targetField){
        Debug.Log(cardSlot.fieldReference.getPosition().x +" "+ cardSlot.fieldReference.getPosition().y);
        cardSlot.fieldReference.setCreature(null);
        cardSlot.fieldReference = targetField;
        Debug.Log(cardSlot.fieldReference.getPosition().x +" "+ cardSlot.fieldReference.getPosition().y);
        targetField.setCreature(this);
    }
    
    //************************************************************** Fighting Methods
    /// <summary> initiates an Attack against a given Creature.
    /// There are no validation-checks at all! </summary>
    /// <param name="enemy"></param>
    public void attack(Creature enemy){
        Debug.Log(cardReference.getName() +" is attacking "+ enemy.cardReference.getName());
        
        //TODO DMG calculation
        // DMG results in BaseStr * weaknessMod 
        double damage = strength;
        Debug.Log(" The Attack starts at InitialDamge: "+ damage);
        damage *= enemy.cardReference.checkWeakness(cardReference.AttackType);
        Debug.Log(cardReference.getName() +" takes RealDamge: "+ damage);

        enemy.takeDamage(damage, this);
    }

    public void takeDamage(double incommingDamage, Creature attacker){
        if (incommingDamage < armor){
            armor -= (int) Math.Ceiling(incommingDamage/2);
        } else{
            double finalDamage = incommingDamage - armor;
            armor -= (int)incommingDamage;
            life  -= (int)finalDamage;
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
    public override PlacableCard getReference(){
        return cardReference;
    }

    public int getMovement(){
        return movement;
    }

    /// <summary> Returns the LAST AttackEffect from the AttackEffectList
    /// that is from the given TYPE. </summary>
    /// <param name="type"></param>
    /// <returns> NULL if there is no such Effect. </returns>
    public AttackEffect getAttackEffects(AttackEffectTypeEnum type){
        AttackEffect result = null;
        int i = attackEffects.Count-1;
        while (result == null && i >= 0){
            if (attackEffects[i].Type == type){
                result = attackEffects[i];
            }

            i--;
        }

        return result;
    }
}
