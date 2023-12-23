using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

/// <summary> Building to spawn Creatures </summary>
public class Factory : Building {

   [SerializeField] private List<CreatureType> creatureWhitelist;
   [SerializeField] private List<CreatureType> creatureBlacklist;
   
   public override void initialize(CardSlot cardSlot){
      base.initialize(cardSlot);
      FactoryCard factoryCard = (FactoryCard) cardReference;
      creatureWhitelist = factoryCard.CreatureWhitelist;
      creatureBlacklist = factoryCard.CreatureBlacklist;
   }
   
   //************************************************************** functional Methods
   /// <summary> A creature can only be spawned if its creatureType is on the whitelist OR NOT on the Blacklist.
   /// The Blacklist is only considered if the whitelist is empty. </summary>
   /// <param name="creatureCard"></param>
   /// <returns> TRUE if the creatureCard is compatible with this factory </returns>
   public bool checkCompatibility(CreatureCard creatureCard){
      List<CreatureType> checkValues = creatureCard.getCreatureTypes();
      bool result;
      if (creatureWhitelist.Count > 0){
         result = false;
         foreach (CreatureType checkType in checkValues){
            if (creatureWhitelist.Contains(checkType)){
               result = true;
            }
         }
      } else{
         result = true;
         foreach (CreatureType checkType in checkValues){
            if (creatureBlacklist.Contains(checkType)){
               result = false;
            }
         }
      }
      return result;
   }
   
   //************************************************************** private Methods
   
   //************************************************************************************************* Getter & Setters
   
}
