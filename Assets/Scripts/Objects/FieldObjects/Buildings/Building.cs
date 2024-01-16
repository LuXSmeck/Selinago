using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

public class Building : AFieldObject {

   [Header("Card Attributes")]
   [SerializeField] protected BuildingCard cardReference;
   [SerializeField] protected CardSlot cardSlot;

   [Header("Building Attributes")]
   [SerializeField] private int durability;

   [Header("Creature Effects")]
   [SerializeField] protected List<AUpgradeEffect> upgradeEffects;

   public virtual void initialize(CardSlot cardSlot){
      this.cardSlot = cardSlot;
      cardReference = (BuildingCard)cardSlot.cardReference;
      upgradeEffects = new List<AUpgradeEffect>(cardReference.UpgradeEffects);

      //TODO loyality: Buildings can now be destroyed, but should they be capturable?
      durability = cardReference.Durability;
   }

   public void destroy(){
      Destroy(this);
   }

   //************************************************************** Fighting Methods
   public void increaseDurability(int boost){
      durability += boost;
   }
   
   public void takeDamage(int damage) {
      durability -= damage;
      if (durability <= 0) {
         destroyed();
      }
   }

   //************************************************************** private Methods
   private void destroyed() {
      Debug.Log(cardReference.getName() +" got destroyed");
      cardSlot.removeCard();
   }

   //************************************************************************************************* Getter & Setters
   public override PlacableCard getReference() {
      return cardReference;
   }

   /// <summary> Adds an effect to the building </summary>
   /// <param name="effect"></param>
   public void addUpgradeEffeckt(AUpgradeEffect effect){
      upgradeEffects.Add(effect);
   }
   
   /// <summary> Removes an effect from the building.</summary>
   /// <param name="effect"></param>
   public void removeUpgradeEffeckt(AUpgradeEffect effect){
      bool found = false;
      int i = upgradeEffects.Count-1;
      while (!found && i >= 0){
         if (upgradeEffects[i] == effect){
            upgradeEffects.RemoveAt(i);
            found = true;
         }

         i--;
      }
   }
}
