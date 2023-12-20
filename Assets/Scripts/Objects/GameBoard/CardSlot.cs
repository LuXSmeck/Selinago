using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour{

   [SerializeField] public PlacableCard cardReference;
   [SerializeField] public Field fieldReference;
   
   /// <summary> Checks if the given Field is empty and calls then the placeCardMethod of the given card </summary>
   /// <param name="card"> A placable Card that stays in the active Slot </param>
   /// <param name="field"> The fieldreference to the specific Field </param>
   /// <returns> FALSE if the placement went wrong </returns>
   public bool placeCard(PlacableCard card, Field field){
      if (cardReference != null){
         Debug.LogError("Cardslot already taken!");
         return false;
      } else if (card == null){
         Debug.LogError("no card referenced!");
         return false;
      } else{
         cardReference  = card;
         fieldReference = field;
         bool success = card.placeCard(this);
         if (!success){
            cardReference  = null;
            fieldReference = null;
         }

         return success;
      }
   }
   
   /// <summary> Checks if the given Field is empty and calls then the placeCardMethod of the given card </summary>
   /// <param name="card"> A placable Card that stays in the active Slot </param>
   /// <param name="x"></param>
   /// <param name="y">The fieldcoordinates to the specific Field </param>
   /// <returns> FALSE if the placement went wrong </returns>
   public bool placeCard(PlacableCard card, int x, int y){
      return placeCard(card, CardManager.Instance.getFieldAt(x,y));
   }

   public bool removeCard(){
      if (cardReference != null){
         cardReference.removeCard(this);
         cardReference = null;
         fieldReference = null;
         return true;
      } else{
         return false;
      }
   }
}
