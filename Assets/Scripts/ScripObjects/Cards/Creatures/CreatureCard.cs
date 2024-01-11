using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Creatures/Creature", fileName = "Creature")]
public class CreatureCard : PlacableCard {

   [SerializeField] private GameObject creatureModel;

   [Header("Creature Stats")]
   [SerializeField] private int atk;
   [SerializeField] private int def;

   [Header("Creature Effects")]
   [SerializeField] private List<AAttackEffect> attackEffects;

   [Header("Creature Attributes")]
   [Tooltip("Defines the Class or Subspecies of the Creature, like Fish or Soldier")]
   [SerializeField] protected SubType classType;
   [Tooltip("Defines the Element whith which the Creature attacks")]
   [SerializeField] private ElementType attackType;
   [Tooltip("Defines the SpeciesType of the Creature, like Humanoid or Undead")]
   [SerializeField] protected CreatureType creatureType;

   /// <summary>
   ///     Checks how many damage an Attack of the given Type would do,
   ///     and Returns the calculated factor.
   /// </summary>
   /// <param name="attackType"> ElementType of the incoming Attack </param>
   /// <returns> Double DamageFactor (1 = 100%) </returns>
   public virtual double checkWeakness(ElementType attackType) {
      double dmgFactor = 1;
      if (creatureType.elementalImmunities.Contains(attackType) ||
          classType.elementalImmunities.Contains(attackType)) {
         dmgFactor /= 4;
      }
      if (creatureType.elementalResistances.Contains(attackType) ||
          classType.elementalResistances.Contains(attackType)) {
         dmgFactor /= 2;
      }
      if (creatureType.elementalWeaknesses.Contains(attackType) ||
          classType.elementalWeaknesses.Contains(attackType)) {
         dmgFactor *= 2;
      }

      return dmgFactor;
   }
   
   public override bool placeCard(CardSlot cardSlot){
      Field targetField = cardSlot.fieldReference;
      Factory factory = targetField.getFactory();
      if (!targetField.isApproachable() || 
          factory == null || !factory.checkCompatibility(this)){
         Debug.LogError("field is not Approachable!");
         return false;
         
      }else{
         spawnCreature(cardSlot, targetField, factory);
         return true;
      }
   }

   public bool forcePlaceCard(CardSlot cardSlot){
      Field targetField = cardSlot.fieldReference;
      if (targetField.isApproachable()){
         spawnCreature(cardSlot, cardSlot.fieldReference, null);
         return true;
      } else{
         return false;
      }
   }

   private void spawnCreature(CardSlot cardSlot, Field targetField, Factory factory){
      GameObject instance = instanciateInstance();
      Creature creatureInstance = instance.GetComponent<Creature>();
      creatureInstance.initialize(cardSlot);

      if (factory != null){
         factory.buildNewCreature(creatureInstance);
      }
      targetField.setCreature(creatureInstance);
         
      creatureInstance.name = "Creature: " + cardName;
      if (creatureModel != null){
         Instantiate(creatureModel, instance.transform);
      }
   }

   public override void removeCard(CardSlot cardSlot){
      cardSlot.fieldReference.setCreature(null);
   }

   protected virtual GameObject instanciateInstance(){
      return Instantiate(CardManager.Instance.creatureTemplate, CardManager.Instance.spawnPos);
   }

   public virtual bool checkMyCompatibility(List<Type> checkList){
      bool result = checkList.Contains(creatureType) ||
                    checkList.Contains(classType);

      return result;
   }
   
   //************************************************************************************************* Getter & Setters
   public ElementType AttackType => attackType;
   public int Atk => atk;
   public int Def => def;
   public List<AAttackEffect> AttackEffects => attackEffects;

}
