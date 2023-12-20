using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Fields/Building", fileName = "Building")]
public class BuildingCard : AFieldCard {

   [SerializeField] private GameObject buildingModel;

   [Header("Building Stats")]
   [SerializeField] private int durability;

   public override bool placeCard(CardSlot cardSlot){
      if (!cardSlot.fieldReference.hereCanBeBuild()){
         return false;
      } else{
         /*
         GameObject instance = Instantiate(CardManager.Instance.creatureTemplate,
            CardManager.Instance.spawnPos);
         Creature creatureInstance = instance.GetComponent<Creature>();
         creatureInstance.initialize(this);

         bool result = CardManager.Instance.spawnCreature(targetField, creatureInstance);

         if (result){
            creatureInstance.name = "Creature: " + cardName;

            if (creatureModel != null){
               Instantiate(creatureModel, instance.transform);
            }
         } else{
            Destroy(instance.gameObject);
         }
         */
         
         //TODO
         return true;
      }
   }

   public override void removeCard(CardSlot cardSlot){
      //TODO
   }

   //************************************************************************************************* Getter & Setters
   public int getDurability() {
      return durability;
   }
}
