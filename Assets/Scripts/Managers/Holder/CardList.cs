using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

[System.Serializable]
public class CardList{

   [SerializeField] private int cardLimit_Max;
   [SerializeField] private int cardLimit_Min;
   [SerializeField] private int cardLimit_Doublication;
   [SerializeField] private List<Card> cards;

   public CardList(){
      cardLimit_Min          = 0;
      cardLimit_Max          = int.MaxValue;
      cardLimit_Doublication = int.MaxValue;
      cards                  = new List<Card>();
   }
   
   public CardList(int minLimit, int maxLimit, int doublicationLimit){
      cardLimit_Min          = minLimit;
      cardLimit_Max          = maxLimit;
      cardLimit_Doublication = doublicationLimit;
      cards                  = new List<Card>();
   }

   public CardList(CardList other){
      cardLimit_Min          = other.cardLimit_Min;
      cardLimit_Max          = other.cardLimit_Max;
      cardLimit_Doublication = other.cardLimit_Doublication;
      cards                  = new List<Card>();

      foreach (var card in other.Cards){
         cards.Add(card);
      }
   }

   //********************************************************************************************* Getter & Setters
   public void setLimits(int minLimit, int maxLimit, int doublicationLimit){
      cardLimit_Min          = minLimit;
      cardLimit_Max          = maxLimit;
      cardLimit_Doublication = doublicationLimit;
   }

   public int CardLimitMax => cardLimit_Max;

   public int CardLimitMin => cardLimit_Min;

   public int CardLimitDoublication => cardLimit_Doublication;

   public List<Card> Cards{
      get => cards;
      set => cards = value;
   }

   public int Count{
      get => cards.Count;
   }

   //********************************************************************************************* Methods
   /// <summary>
   /// Checks if adding the card will not make the Cardlist invalid.
   /// If the Deck is already full or it contains already to many copys of this card, the card will NOT be added.
   /// </summary>
   /// <param name="card"></param>
   /// <returns> TRUE if card could be added</returns>
   public bool Add(Card card){
       if (Count < cardLimit_Max){
          int cardCounter = 0;
          foreach (var deckCard in cards){
             if (card == deckCard){
                cardCounter++;
             }
          }
   
          if ((cardCounter < cardLimit_Doublication) && (cardCounter < card.getLimited())){
             cards.Add(card);
             return true;
          } else{
             return false;
          }
       } else{
          return false;
       }
    }

   /// <summary>
   /// Adds the card and ignores any restrictions
   /// </summary>
   /// <param name="card"></param>
   public void AddStrait(Card card){
      cards.Add(card);
   }

   /// <summary>
   /// Returns the first occurence of the Card from the list
   /// </summary>
   /// <param name="card"></param>
   /// <returns> TRUE if a card was removed </returns>
   public bool Remove(Card card){
      return cards.Remove(card);
   }

   /// <summary>
   /// Returns TRUE if the given card is in the List.
   /// </summary>
   /// <param name="value"></param>
   /// <returns></returns>
   public bool Contains(Card card){
      return cards.Contains(card);
   }

   /// <summary>
   /// Checks if a Deck meets its requirements.
   /// This means if there are enough but not to many cards in it,
   /// and if there are not to many copys of one card in it. 
   /// </summary>
   /// <returns> TRUE if all requirements are meet </returns>
   public bool isValid(){
      bool valid = true;

      if ((Count > cardLimit_Max) || (Count < cardLimit_Min)){
         valid = false;
      } else{
         List<Card> uniqueDeck = (List<Card>)cards.Distinct();
         int i = 0;
         
         while (valid && i < uniqueDeck.Count){
            int counter = 0;
            foreach (var card in cards){
               if (uniqueDeck[i] == card){
                  counter++;
               }
            }

            if ((counter > cardLimit_Doublication) || (counter > uniqueDeck[i].getLimited())){
               valid = false;
            }
         }
      }  
      return valid;
   }
   
   /// <summary>
   /// Swaps every single Position in the List with another
   /// </summary>
   public void shuffle(){
      int currentIndex = cards.Count -1;
      //Random random = new Random();
      while (currentIndex >= 0) {
         int rnd = Random.Range(0, cards.Count -1);  
         Card tmpCard = cards[rnd];                // Swap
         cards[rnd] = cards[currentIndex];  
         cards[currentIndex] = tmpCard;    
         currentIndex--;  
      }
   }
}
