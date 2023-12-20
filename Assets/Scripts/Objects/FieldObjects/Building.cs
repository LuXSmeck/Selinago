using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

public class Building : AFieldObject {

   [Header("Card Attributes")]
   [SerializeField] private BuildingCard cardReference;

   [Header("Building Attributes")]
   [SerializeField] private int durability;

   public void initialize(BuildingCard cardReference) {
      this.cardReference = cardReference;

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
      //TODO
   }

   //************************************************************************************************* Getter & Setters
   public override PlacableCard getReference() {
      return cardReference;
   }
}
