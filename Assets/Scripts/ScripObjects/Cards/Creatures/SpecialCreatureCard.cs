using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary> SpecialCreatures are the Bosses of the Game. </summary>
[CreateAssetMenu(menuName = "Cards/Creatures/Special", fileName = "Creature")]
public class SpecialCreatureCard : HybridCard{
    
   public override bool placeCard(CardSlot cardSlot){
      Field targetField = cardSlot.fieldReference;
      Factory factory = targetField.getFactory();
      if (cardSlot is SpecialCardSlot && factory is HQ){
         spawnCreature(cardSlot, targetField, factory);
         return true;
      }else{
         Debug.LogError("Bosses have to be spawned in Base!");
         return false;
      }
   }

}
