using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This Deck represents the cards on the Field. Here are no Deck-Restrictions or LimitChecks.
/// </summary>
[System.Serializable]
public class Deck{

   [Header("Deck")] //Those Lists represent the real playable Decks that change during the Game.
   [SerializeField] private CardList creatureDeck;
   [Space]
   [SerializeField] private CardList secondaryDeck;
   [Space]
   [SerializeField] private CardList sideDeck;
   [Space]
   [SerializeField] private CardList graveyard;
   [Space]
   [SerializeField] private CardList banished;
   
   public enum DeckIndicator { MAINDECK, SECUNDARYDECK, SIDEDECK, GRAVEYARD, BANISHED};
   
   public Deck(DeckList decklist){
      prepareDecks(decklist);
   }

   public CardList CreatureDeck => creatureDeck;
   public CardList SecondaryDeck => secondaryDeck;
   public CardList SideDeck => sideDeck;
   public CardList Graveyard => graveyard;
   public CardList Banished => banished;

   //********************************************************************************************************* Ingame-Methods
   /// <summary>
   /// Initialize all Decks as preparation for a new game.
   /// Main and secondary decks has to be shuffeled,
   /// while the graveyard has to be empty.
   /// </summary>
   public void prepareDecks(DeckList decklist){
      creatureDeck  = decklist.CreatureDeckList;
      secondaryDeck = decklist.SecondaryDeckList;
      sideDeck      = decklist.SideDeckList;
      graveyard     = null;
      banished      = null;
      creatureDeck.shuffle();
      secondaryDeck.shuffle();
   }

   /// <summary>
   /// Takes the FIRST Card from the TOP of "Main"- or "Secondary"-Deck.
   /// That card is removed from the Deck after returning.
   /// </summary>
   /// <param name="indicator"></param>
   /// <returns> A random Card from the given Deck, or
   /// NULL if an invalid Deck was chosen or that deck was empty </returns>
   public Card drawCard(DeckIndicator indicator){
      CardList deck;
      switch (indicator){
         case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
         case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
         default:                          deck = null;           break;
      }

      if (deck != null){
         Card card = deck.Cards[deck.Count];
         deck.Remove(card);

         return card;
      } else{
         return null;
      }
   }

   /// <summary>
   /// Removes a specific Card from the given Deck. The Deck is shuffled afterwards.
   /// Returns FALSE if the deck does not contains that card.
   /// </summary>
   /// <param name="indicator"></param>
   /// <param name="card"></param>
   /// <returns> True if a card could be successfully removed </returns>
   public bool pickCard(DeckIndicator indicator, Card card){
      CardList deck;
      switch (indicator){
         case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
         case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
         case DeckIndicator.SIDEDECK:      deck = sideDeck;       break;
         case DeckIndicator.GRAVEYARD:     deck = graveyard;      break;
         default:                          deck = null;           break;
      }
      
      if ((deck != null) && (deck.Contains(card))){
         deck.Remove(card);
         deck.shuffle();
         return true;
      } else{
         return false;
      }
   }

   /// <summary>
   /// Adds a specific card in a deck and shuffles the deck afterwards
   /// </summary>
   /// <param name="indicator"></param>
   /// <param name="card"></param>
   /// <param name="doShuffle"> (optional default=true) if this is TRUE the deck will shuffled </param>
   public void putCardToDeck(DeckIndicator indicator, Card card, bool doShuffle = true){
      CardList deck;
      switch (indicator){
         case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
         case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
         case DeckIndicator.SIDEDECK:      deck = sideDeck;       break;
         case DeckIndicator.GRAVEYARD:     deck = graveyard;      break;
         case DeckIndicator.BANISHED:      deck = banished;      break;
         default:                          deck = null;           break;
      }
      
      if (deck != null){
         deck.Add(card);

         if (doShuffle){
            deck.shuffle();
         }
      }
   }

   /// <summary>
   /// Counts how many cards are in the given List.
   /// </summary>
   /// <param name="indicator"></param>
   /// <returns> Returns "-1" if an Error occured </returns>
   public int cardsInList(DeckIndicator indicator){
      CardList deck;
      switch (indicator){
         case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
         case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
         case DeckIndicator.SIDEDECK:      deck = sideDeck;       break;
         case DeckIndicator.GRAVEYARD:     deck = graveyard;      break;
         case DeckIndicator.BANISHED:      deck = banished;       break;
         default:                          deck = null;           break;
      }
      if (deck != null){
         return deck.Count;
      } else{
         return -1;
      }
   }

   /// <summary>
   /// Checks if both the Main- and Secondary-Decks have 0 Cards left.
   /// </summary>
   /// <returns></returns>
   public bool isEmpty(){
      return((creatureDeck.Count == 0) && (secondaryDeck.Count == 0));
   }
}
