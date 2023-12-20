using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Fields/Terrain", fileName = "Terrain")]
public class TerrainCard : AFieldCard {

   [SerializeField] private int affectionRange; //TODO search better Name
   [SerializeField] private TerrainFeature terrain;

   public override bool placeCard(CardSlot cardSlot){
      CardManager.Instance.getFields().terraformArea(affectionRange, cardSlot, terrain);
      
      return true;
   }

   public override void removeCard(CardSlot cardSlot){
      List<Field> fieldsInRange = CardManager.Instance.getFields().pathFinder.findFieldsInRange(
                                    (int)cardSlot.fieldReference.getPosition().x,
                                    (int)cardSlot.fieldReference.getPosition().y,
                                    affectionRange);
                                      
      foreach (Field field in fieldsInRange) {
         if (field.getArea().getCardSlot() == cardSlot) {
            field.getArea().removeSource();
         }
      }
   }


   //************************************************************************************************* Getter & Setters
   // public Color getTerrainColor(){
   //    return terrain.getColor();
   // }
   
}
