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

   public override bool placeCard(CardSlot cardSlot, bool forcePlace=false){
      if (!cardSlot.fieldReference.hereCanBeBuild()){
         return false;
      } else{
         GameObject instance = instanciateInstance();
         Building buildingInstance = instance.GetComponent<Building>();
         buildingInstance.initialize(cardSlot);

         bool result = CardManager.Instance.spawnBuilding(cardSlot.fieldReference, buildingInstance);

         if (result){
            buildingInstance.name = "Building: " + cardName;

            if (buildingModel != null){
               Instantiate(buildingModel, instance.transform);
            }
            return true;
         } else{
            Destroy(instance.gameObject);
            return false;
         }
      }
   }


   public override void removeCard(CardSlot cardSlot){
      //TODO
   }
   
   protected virtual GameObject instanciateInstance(){
      return Instantiate(CardManager.Instance.buildingTemplate, CardManager.Instance.spawnPos);
   }

   //************************************************************************************************* Getter & Setters
   public int getDurability() {
      return durability;
   }
}
