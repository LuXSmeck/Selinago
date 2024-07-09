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
   [Tooltip("Defines the Element with which the Creature attacks")]
   [SerializeField] private ElementType attackType;
   [Tooltip("Defines the SpeciesType of the Creature, like Humanoid or Undead")]
   [SerializeField] protected CreatureType creatureType;

   /// <summary> Checks how many damage an Attack of the given Type would do,
   ///     and Returns the calculated factor. </summary>
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
   
   /// <summary> Forward called from a placable Cardslot!
   /// Checks if the Creature can be spawned here and does so if possible. </summary>
   /// <param name="cardSlot"></param>
   /// <returns> FALSE if the creature can't be spawned in the Target location. </returns>
   public override bool placeCard(CardSlot cardSlot){
      Field targetField = cardSlot.fieldReference;
      Factory factory = targetField.getFactory();
      if (!targetField.isApproachable() || cardSlot is SpecialCardSlot ||
          factory == null || !factory.checkCompatibility(this)){
         Debug.LogError("field is not Approachable!");
         return false;
         
      }else{
         spawnCreature(cardSlot, targetField, factory);
         return true;
      }
   }

   /// <summary> Initialize the Spawning of the new Creature and sets all links.
   /// There are no more checks for Compability or NullPointers of parameters </summary>
   /// <param name="cardSlot"></param>
   /// <param name="targetField"></param>
   /// <param name="factory"></param>
   protected void spawnCreature(CardSlot cardSlot, Field targetField, Factory factory){
      GameObject instance = instanciateInstance();
      Creature creatureInstance = instance.GetComponent<Creature>();
      
      creatureInstance.initialize(cardSlot);
      factory.buildNewCreature(creatureInstance);
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

   /// <summary> Checks if the Creature 
   /// </summary>
   /// <param name="checkList"></param>
   /// <returns></returns>
   public virtual bool checkMyCompatibility(List<Type> checkList){
      bool result = checkList.Contains(creatureType) ||
                    checkList.Contains(classType);

      return result;
   }
   
   public virtual bool checkMyCompatibility(SubType pClassType, CreatureType pCreatureType){
      bool result = classType == pClassType ||
                    creatureType == pCreatureType;

      return result;
   }
   public virtual bool checkMyCompatibility(SubType pClassType, ElementType pAttackType, CreatureType pCreatureType){
      bool result = classType == pClassType ||
                    attackType == pAttackType ||
                    creatureType == pCreatureType;

      return result;
   }
   
   //************************************************************************************************* Getter & Setters
   public ElementType AttackType => attackType;
   public int Atk => atk;
   public int Def => def;
   public List<AAttackEffect> AttackEffects => attackEffects;

}
