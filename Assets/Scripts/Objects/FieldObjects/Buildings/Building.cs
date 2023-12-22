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

   public virtual void initialize(CardSlot cardSlot){
      this.cardSlot = cardSlot;
      cardReference = (BuildingCard)cardSlot.cardReference;

      //TODO loyality: Buildings can now be destroyed, but should they be capturable?
      durability = cardReference.getDurability();
   }

   public void destroy(){
      Destroy(this);
   }

   //************************************************************** Fighting Methods
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
}
