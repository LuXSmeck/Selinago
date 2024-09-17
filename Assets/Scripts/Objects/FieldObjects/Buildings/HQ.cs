using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

/// <summary> MainBuilding of a Player </summary>
public class HQ : Factory {
   
   [SerializeField] private List<Type> creatureBufflist;

   public override void initialize(CardSlot cardSlot){
      base.initialize(cardSlot);
      HQCard factoryCard = (HQCard) cardReference;
      creatureBufflist = new List<Type>(factoryCard.CreatureBufflist);
   }
   
   /// <summary> A Creature build in HQ can receive some Effects, but only
   /// supported Creatures will receive those. </summary>
   /// <param name="creature"></param>
   public override void buildNewCreature(Creature creature){
      CreatureCard creatureCard = (CreatureCard)creature.getReference();
      if (creatureCard.checkMyCompatibility(creatureBufflist)){
         base.buildNewCreature(creature);
      }
   }
   
   //************************************************************** functional Methods
   /// <summary> An HQ can build any creature </summary>
   /// <param name="creatureCard"></param>
   /// <returns> TRUE </returns>
   public override bool checkCompatibility(CreatureCard creatureCard){
      return true;
   }
   
   //************************************************************** private Methods
   //
   //************************************************************************************************* Getter & Setters
   
}
