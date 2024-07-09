using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Creatures/Fusion", fileName = "Creature")]
public class FusionCreatureCard : HybridCard{
   
   [Header("Creature Requirements")]
   [Tooltip("Defines the Class or Subspecies of the MainCreature, that will be sacrificed")]
   [SerializeField] private SubType neededClassOfMainCreature;
   [Tooltip("Defines the Class or Subspecies of the SecundaryCreature, that will be sacrificed")]
   [SerializeField] private SubType neededClassOfSecCreature;
   [Tooltip("Defines the Element with which the SecundaryCreature attacks")]
   [SerializeField] private ElementType neededAttackTypeOfSecCreature;
   [Tooltip("Defines the SpeciesType of the MainCreature")]
   [SerializeField] private CreatureType neededTypeOfMainCreature;
   [Tooltip("Defines the SpeciesType of the SecundaryCreature")]
   [SerializeField] private CreatureType neededTypeOfSecCreature;

   
   public override bool placeCard(CardSlot cardSlot){
      Field targetField = cardSlot.fieldReference;
      CreatureCard creatureCard = (CreatureCard)targetField.getCreature().getReference();
      if (cardSlot is SpecialCardSlot){
         //TODO Check if Main Creature matches
         creatureCard.checkMyCompatibility(neededClassOfMainCreature, null, neededTypeOfMainCreature);
         // Check if Sec Creature matches and is in range
         
         // Save old StatChanges in GMFactory
         
         // Delete old Creatures
         
         //TODO Spawn new Creature with GM Factory

      /*
         Factory fusionFactory = new Factory();
         spawnCreature(cardSlot, targetField, factory);
      */
         return true;
      
      }else{
         Debug.LogError("Bosses have to be spawned in Base!");
         return false;
      }
   }
    
}
