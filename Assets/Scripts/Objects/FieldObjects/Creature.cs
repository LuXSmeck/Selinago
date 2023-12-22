using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;
using Object = UnityEngine.Object;

public class Creature : AFieldObject{

    [Header("Card Attributes")]
    [SerializeField] protected CreatureCard cardReference;
    [SerializeField] protected CardSlot cardSlot;
    
    [Header("Creature Attributes")]
    [SerializeField] private   int strength;
    [SerializeField] protected int armor;
    [SerializeField] private   int movement;
    [SerializeField] protected CreatureStateEnum state;
    
    [Header("Creature Effects")]
    [SerializeField] private List<AAttackEffect> attackEffects;
    
    public virtual void initialize(CardSlot cardSlot){
        this.cardSlot = cardSlot;
        cardReference = (CreatureCard)cardSlot.cardReference;

        strength      = cardReference.Atk;
        armor         = cardReference.Def;
        attackEffects = cardReference.AttackEffects;
        
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
        
        // DMG results in BaseStr * weaknessMod 
        double damage = strength;
        damage *= enemy.cardReference.checkWeakness(cardReference.AttackType);
        Debug.Log(" The Attack starts at InitialDamge: "+ strength +" -> "+ damage +"Damge after resisting");

        // Triggering OnHitEffects and/or triggering takeDamage Method of the enemy Creature
        List<AAttackEffect> onHitEffects = getAttackEffects(AttackEffectTypeEnum.ONHIT);
        bool attackSequence = true;
        while (onHitEffects.Count > 0 && attackSequence){
            AOnHitEffect effect = (AOnHitEffect)onHitEffects[0];
            Debug.Log(effect.name +" got triggered");
            attackSequence = effect.modifiedTakeDamage(damage, this, enemy);
            onHitEffects.RemoveAt(0);
        }

        if (attackSequence){
            enemy.takeDamage(damage, this);
        }
    }

    public virtual void takeDamage(double incommingDamage, Creature attacker){
        if (incommingDamage >= armor){
            double finalDamage = incommingDamage - armor;
            armor     -= (int) Math.Ceiling(incommingDamage/2);
            strength  -= (int) Math.Ceiling(finalDamage);
            Debug.Log(" The Attack penetrated the Armor of "+ cardReference.getName() +" and lowered the strength by"+ finalDamage);
        }else{
            int finalDamage = (int) Math.Ceiling(incommingDamage/3);
            armor -= finalDamage;
            Debug.Log(" The Attack didn't penetrated the Armor of "+ cardReference.getName() +" but lowered it by"+ finalDamage);
        }
 
        checkLife();
        if (state != CreatureStateEnum.DEAD){
            foreach (AAfterHitEffect effect in attacker.getAttackEffects(AttackEffectTypeEnum.AFTERHIT)){
                effect.performEffect(this); 
            }
        }
    }
    
    public void die(){
        state = CreatureStateEnum.DEAD;
        Debug.Log(cardReference.getName() +" failted");
        cardSlot.removeCard();
    }
    
    //************************************************************** private Methods
    /// <summary> If the Creatures STR is lower than 0, the missing STR will be subtracted as OVERDMG from the Armor
    /// If the STR and the Armor are 0 or lower, the Creature dies. </summary>
    protected virtual void checkLife(){
        if (strength < 0){
            armor += strength;
            strength = 0;
        }
        
        if (strength + armor <= 0){
            die();
        } else if (armor < 0){
            strength += armor;
            armor = 0;
        }
    }

    //************************************************************************************************* Getter & Setters
    public int Strength => strength;
    public int Armor => armor;
    public int Movement => movement;
    public override PlacableCard getReference(){
        return cardReference;
    }
    
    public bool isDead(){
        return state == CreatureStateEnum.DEAD;
    }

    /// <summary> This Setter is used to simulate DAMAGE. Don't use it do modify STR in any other Situation! </summary>
    /// <param name="damageValue"> This value will be subtracted.</param>
    public virtual void damageStrength(int damageValue){
        strength -= damageValue;
    }
    
    /// <summary> This Setter is used to simulate DAMAGE. Don't use it do modify DEF in any other Situation! </summary>
    /// <param name="damageValue"> This value will be subtracted. </param>
    public virtual void damageArmor(int damageValue){
        armor -= damageValue;
    }
    
    /// <summary> Returns all AttackEffects from the AttackEffectList
    /// that is from the given TYPE. The List will be sorted by the priority. </summary>
    /// <param name="type"></param>
    /// <returns> NULL if there is no such Effect. </returns>
    public List<AAttackEffect> getAttackEffects(AttackEffectTypeEnum type){
        List<AAttackEffect> liResult = new List<AAttackEffect>();
        for (int i=0; i < attackEffects.Count; i++){
            if (attackEffects[i].Type == type){
                liResult.Add(attackEffects[i]);
            }
        }
        liResult.Sort();
        return liResult;
    }
    
    /// <summary> Returns the LAST AttackEffect from the AttackEffectList
    /// that is from the given TYPE. </summary>
    /// <param name="type"></param>
    /// <returns> NULL if there is no such Effect. </returns>
    public AAttackEffect getLastAttackEffect(AttackEffectTypeEnum type){
        AAttackEffect result = null;
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
