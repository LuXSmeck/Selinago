using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Creatures/Creature", fileName = "Creature")]
public class CreatureCard : PlacableCard {

   [SerializeField] private GameObject creatureModel;

   [Header("Creature Attributes")]
   [Tooltip("Defines the SpeciesType of the Creature, like Humanoid or Undead")]
   [SerializeField] protected CreatureType creatureType;
   [Tooltip("Defines the Class or Subspecies of the Creature, like Fish or Soldier")]
   [SerializeField] protected SubType classType;
   [Tooltip("Defines the Element whith which the Creature attacks")]
   [SerializeField] private ElementType attackType;

   [Header("Creature Stats")]
   [SerializeField] private int hp;
   [SerializeField] private int atk;
   [SerializeField] private int def;

   [Header("Creature Effects")]
   [SerializeField] private List<AAttackEffect> attackEffects;
   
   /// <summary>
   ///     Checks how many damage an Attack of the given Type would do,
   ///     and Returns the calculated factor.
   /// </summary>
   /// <param name="pAttackType"> ElementType of the incoming Attack </param>
   /// <returns> Double DamageFactor (1 = 100%) </returns>
   public virtual double checkWeakness(ElementType pAttackType) {
      double dmgFactor = 1;
      if (creatureType.elementalImmunities.Contains(pAttackType) ||
          classType.elementalImmunities.Contains(pAttackType)) {
         dmgFactor /= 4;
      }
      if (creatureType.elementalResistances.Contains(pAttackType) ||
          classType.elementalResistances.Contains(pAttackType)) {
         dmgFactor /= 2;
      }
      if (creatureType.elementalWeaknesses.Contains(pAttackType) ||
          classType.elementalWeaknesses.Contains(pAttackType)) {
         dmgFactor *= 2;
      }

      return dmgFactor;
   }

   public override bool placeCard(CardSlot cardSlot){
      if (!cardSlot.fieldReference.isApproachable()){
         Debug.LogError("field is not Approachable!");
         return false;
      } else{
         GameObject instance = Instantiate(CardManager.Instance.creatureTemplate,
                                           CardManager.Instance.spawnPos);
         Creature creatureInstance = instance.GetComponent<Creature>();
         creatureInstance.initialize(cardSlot);

         bool result = CardManager.Instance.spawnCreature(cardSlot.fieldReference, creatureInstance);

         if (result){
            creatureInstance.name = "Creature: " + cardName;

            if (creatureModel != null){
               Instantiate(creatureModel, instance.transform);
            }
         } else{
            Destroy(instance.gameObject);
         }

         return true;
      }
   }

   public override void removeCard(CardSlot cardSlot){
      cardSlot.fieldReference.setCreature(null);
   }

   //************************************************************************************************* Getter & Setters
   public ElementType AttackType => attackType;

   public int Hp => hp;

   public int Atk => atk;

   public int Def => def;

   public List<AAttackEffect> AttackEffects => attackEffects;
}
