using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This Deck represents the theoretical card-collection before a game starts.
/// </summary>
[System.Serializable]
public class DeckList{

   [Header("CardLists")] //Those Lists represent the Deck in it's "full"-state. 
   [SerializeField] private CardList creatureDeckList;  //This list contains all CreatureCards as the mainDeck.
   [Space]
   [SerializeField] private CardList secondaryDeckList; //This List contains all Field- and EquipmentCards as the secondaryDeck.
   [Space]
   [SerializeField] private CardList sideDeckList;      //The sideDeck contains cards that can't be drawn, and will enter the game via different special Effects.

   public bool isValidDeck;

   public DeckList(){
      creatureDeckList  = new CardList();
      secondaryDeckList = new CardList();
      sideDeckList      = new CardList();
   }
   
   public DeckList(Dictionary<String, int> cardLimitations){
      creatureDeckList  = new CardList(cardLimitations["MainDeck_MinCards"], cardLimitations["MainDeck_MaxCards"], cardLimitations["MainDeck_Doubles"]);
      secondaryDeckList = new CardList(cardLimitations["SecDeck_MinCards"], cardLimitations["SecDeck_MaxCards"], cardLimitations["SecDeck_Doubles"]);
      sideDeckList      = new CardList(cardLimitations["SideDeck_MinCards"], cardLimitations["SideDeck_MaxCards"], cardLimitations["SideDeck_Doubles"]);
   }

   public CardList CreatureDeckList => creatureDeckList;
   public CardList SecondaryDeckList => secondaryDeckList;
   public CardList SideDeckList => sideDeckList;

   //********************************************************************************************************* Deck-Modification Methods
   /// <summary>
   /// Adds a card to the correspondingDeck.
   /// </summary>
   /// <param name="card"></param>
   /// <param name="validCheck"> optional (Default false), If TRUE, The card can only be added it the deck will be valid after adding </param>
   public void addCardToDeckList(Card card, bool validCheck = false){
      CardList deck = null;
      if ((card is AFieldCard) || (card is EquipableCard)){
         deck = secondaryDeckList;
      } else if ((card is CreatureCard) && !((card is FusionCreatureCard) || (card is SpecialCreatureCard))){
         deck = creatureDeckList;
      } else if (card is CreatureCard){
         deck = sideDeckList;
      } else{
         Debug.LogError("Error 4001: no matching Deck found");
      }

      if (deck != null){
         if (validCheck){
            deck.Add(card);
         } else{
            deck.AddStrait(card);
         }
      }
   }

   /// <summary>
   /// Tries to remove the card from the Main,
   /// then the secondary and then from the SideDeck
   /// </summary>
   /// <param name="card"></param>
   public void removeCardFromDeckList(Card card){
      if (!creatureDeckList.Remove(card)){
         if (!secondaryDeckList.Remove(card)){
            sideDeckList.Remove(card);
         }
      }
   }
   
   /// <summary>
   /// Checks if all 3 Decks meet their requirements
   /// </summary>
   /// <returns> TRUE if all 3 are valid </returns>
   public bool areDecksValid(){
      isValidDeck = creatureDeckList.isValid() && secondaryDeckList.isValid() && sideDeckList.isValid();
      return isValidDeck;
   }
}
