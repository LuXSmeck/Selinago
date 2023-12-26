using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

/// <summary> Building to spawn Creatures </summary>
public class Factory : Building {

   [SerializeField] private List<Type> creatureWhitelist;
   [SerializeField] private List<Type> creatureBlacklist;
   
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
      bool result;
      if (creatureWhitelist.Count > 0){
         result = creatureCard.checkMyCompatibility(creatureWhitelist);
      } else{
         result = ! creatureCard.checkMyCompatibility(creatureBlacklist);
      }
      return result;
   }
   
   //************************************************************** private Methods
   
   //************************************************************************************************* Getter & Setters
   
}
