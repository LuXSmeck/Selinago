// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
//
// [CreateAssetMenu(menuName = "Cards/Deck", fileName = "Deck")]
// public class Deck : ScriptableObject{
//
//    [Header("CardLists")] //Those Lists represent the Deck in it's "full"-state. They are only modifiable outside of a Game.
//    [SerializeField] private CardList creatureDeckList;  //This list contains all CreatureCards as the mainDeck.
//    [SerializeField] private CardList secondaryDeckList; //This List contains all Field- and EquipmentCards as the secondaryDeck.
//    [SerializeField] private CardList sideDeckList;      //The sideDeck contains cards that can't be drawn, and will enter the game via different special Effects.
//
//    [Header("Deck")] //Those Lists represent the real playable Decks that change during the Game.
//    private CardList creatureDeck;
//    private CardList secondaryDeck;
//    private CardList sideDeck;
//    private CardList graveyard;
//
//    public enum DeckIndicator { MAINDECK, SECUNDARYDECK, SIDEDECK, GRAVEYARD};
//    
//    public Deck(){
//       Dictionary<String, int> cardLimitations = GameManager.Instance.cardLimitations;
//       creatureDeckList  = new CardList(cardLimitations["MAINDECK_MINCARDS"], cardLimitations["MAINDECK_MAXCARDS"], cardLimitations["MAINDECK_DOUBLES"]);
//       secondaryDeckList = new CardList(cardLimitations["SECDECK_MINCARDS"], cardLimitations["SECDECK_MAXCARDS"], cardLimitations["SECDECK_DOUBLES"]);
//       sideDeckList      = new CardList(cardLimitations["SIDEDECK_MINCARDS"], cardLimitations["SIDEDECK_MAXCARDS"], cardLimitations["SIDEDECK_DOUBLES"]);
//       
//       creatureDeck  = new CardList();
//       secondaryDeck = new CardList();
//       sideDeck      = new CardList();
//    }
//
//    public CardList CreatureDeck => creatureDeck;
//
//    public CardList SecondaryDeck => secondaryDeck;
//
//    public CardList SideDeck => sideDeck;
//
//    public CardList Graveyard => graveyard;
//
//    //********************************************************************************************************* Ingame-Methods
//    /// <summary>
//    /// Initialize all Decks as preparation for a new game.
//    /// Main and secondary decks has to be shuffeled,
//    /// while the graveyard has to be empty.
//    /// </summary>
//    public void prepareDecks(){
//       creatureDeck  = new CardList(creatureDeckList);
//       secondaryDeck = new CardList(secondaryDeckList);
//       sideDeck      = new CardList(sideDeckList);
//       creatureDeck.shuffle();
//       secondaryDeck.shuffle();
//       graveyard     = null;
//    }
//
//    /// <summary>
//    /// Takes the FIRST Card from the TOP of "Main"- or "Secondary"-Deck.
//    /// That card is removed from the Deck after returning.
//    /// </summary>
//    /// <param name="indicator"></param>
//    /// <returns> A random Card from the given Deck, or
//    /// NULL if an invalid Deck was chosen or that deck was empty </returns>
//    public Card drawCard(DeckIndicator indicator){
//       CardList deck;
//       switch (indicator){
//          case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
//          case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
//          default:                          deck = null;           break;
//       }
//
//       if (deck != null){
//          Card card = deck[deck.Count];
//          deck.Remove(card);
//
//          return card;
//       } else{
//          return null;
//       }
//    }
//
//    /// <summary>
//    /// Removes a specific Card from the given Deck. The Deck is shuffled afterwards.
//    /// Returns FALSE if the deck does not contains that card.
//    /// </summary>
//    /// <param name="indicator"></param>
//    /// <param name="card"></param>
//    /// <returns> True if a card could be successfully removed </returns>
//    public bool pickCard(DeckIndicator indicator, Card card){
//       CardList deck;
//       switch (indicator){
//          case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
//          case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
//          case DeckIndicator.SIDEDECK:      deck = sideDeck;       break;
//          case DeckIndicator.GRAVEYARD:     deck = graveyard;      break;
//          default:                          deck = null;           break;
//       }
//       
//       if ((deck != null) && (deck.Contains(card))){
//          deck.Remove(card);
//          deck.shuffle();
//          return true;
//       } else{
//          return false;
//       }
//    }
//
//    /// <summary>
//    /// Adds a specific card in a deck and shuffles the deck afterwards
//    /// </summary>
//    /// <param name="indicator"></param>
//    /// <param name="card"></param>
//    /// <param name="doShuffle"> (optional default=true) if this is TRUE the deck will shuffled </param>
//    public void putCardToDeck(DeckIndicator indicator, Card card, bool doShuffle = true){
//       CardList deck;
//       switch (indicator){
//          case DeckIndicator.MAINDECK:      deck = creatureDeck;   break;
//          case DeckIndicator.SECUNDARYDECK: deck = secondaryDeck;  break;
//          case DeckIndicator.SIDEDECK:      deck = sideDeck;       break;
//          case DeckIndicator.GRAVEYARD:     deck = graveyard;      break;
//          default:                          deck = null;           break;
//       }
//       
//       if (deck != null){
//          deck.Add(card);
//
//          if (doShuffle){
//             deck.shuffle();
//          }
//       }
//    }
//
//    /// <summary>
//    /// Checks if both the Main- and Secondary-Decks have 0 Cards left.
//    /// </summary>
//    /// <returns></returns>
//    public bool isEmpty(){
//       return((creatureDeck.Count == 0) && (secondaryDeck.Count == 0));
//    }
//    
//    //********************************************************************************************************* Deck-Modification Methods
//    /// <summary>
//    /// Adds a card to the correspondingDeck.
//    /// </summary>
//    /// <param name="card"></param>
//    public void addCardToDeckList(Card card, bool validCheck=false){
//       CardList deck = null;
//       if ((card is FieldCard) || (card is EquipableCard)){
//          deck = secondaryDeckList;
//       } else if ((card is CreatureCard) && !((card is FusionCreatureCard) || (card is SpecialCreatureCard))){
//          deck = creatureDeckList;
//       } else if (card is CreatureCard){
//          deck = sideDeckList;
//       } else{
//          Debug.LogError("Error 4001: no matching Deck found");
//       }
//
//       if (deck != null){
//          if (validCheck){
//             tryToAddCardToDeck(deck, card);
//          } else{
//             deck.Add(card);
//          }
//       }
//    }
//    
//    /// <summary>
//    /// Checks if all 3 Decks meet their requirements
//    /// </summary>
//    /// <returns> TRUE if all 3 are valid </returns>
//    public bool areDecksValid(){
//       return isDeckValid(creatureDeckList) && isDeckValid(secondaryDeckList) && isDeckValid(sideDeckList);
//    }
//
//    /// <summary>
//    /// Tries to add a card to the given CardList.
//    /// If the Deck is already full or it contains already to many copys of this card, the card will NOT be added.
//    /// </summary>
//    /// <param name="deck"></param>
//    /// <param name="card"></param>
//    /// <returns> TRUE if the card could be added successfully </returns>
//    private bool tryToAddCardToDeck(CardList deck, Card card){
//       if (deck.Count < deck.CardLimitMax){
//          int cardCounter = 0;
//          foreach (var deckCard in deck){
//             if (card == deckCard){
//                cardCounter++;
//             }
//          }
//
//          if ((cardCounter < deck.CardLimitDoublication) && (cardCounter < card.getLimited())){
//             deck.Add(card);
//             return true;
//          } else{
//             return false;
//          }
//       } else{
//          return false;
//       }
//    }
//  
//    /// <summary>
//    /// Checks if a Deck meets its requirements.
//    /// This means if there are enough but not to many cards in it,
//    /// and if there are not to many copys of one card in it. 
//    /// </summary>
//    /// <param name="deck"></param>
//    /// <returns> TRUE if all requirements are meet </returns>
//    private bool isDeckValid(CardList deck){
//       bool valid = true;
//
//       if ((deck.Count > deck.CardLimitMax) || (deck.Count < deck.CardLimitMin)){
//          valid = false;
//       } else{
//          CardList uniqueDeck = (CardList)deck.Distinct();
//          int i = 0;
//          
//          while (valid && i < uniqueDeck.Count){
//             int counter = 0;
//             foreach (var card in deck){
//                if (uniqueDeck[i] == card){
//                   counter++;
//                }
//             }
//
//             if ((counter > deck.CardLimitDoublication) || (counter > uniqueDeck[i].getLimited())){
//                valid = false;
//             }
//          }
//       }  
//       return valid;
//    }
// }
