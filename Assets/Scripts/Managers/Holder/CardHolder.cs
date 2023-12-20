using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour{

   [Header("All available Cards")]
   public List<CreatureCard> creatureCards;
   public List<AFieldCard>    fieldCards;
   public List<EquipableCard>   equipmentCards;
   public List<Deck> decks;

   private void Start(){
      
   }
}
