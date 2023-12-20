// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor.UIElements;
// using UnityEngine;
//
// [System.Serializable]
// public class CardList : List<Card>{
//
//    [SerializeField] private int cardLimit_Max;
//    [SerializeField] private int cardLimit_Min;
//    [SerializeField] private int cardLimit_Doublication;
//    [SerializeField] private List<Card> myList;
//
//    public CardList(){
//       cardLimit_Min          = 0;
//       cardLimit_Max          = int.MaxValue;
//       cardLimit_Doublication = int.MaxValue;
//    }
//    
//    public CardList(int minLimit, int maxLimit, int doublicationLimit) : base(){
//       cardLimit_Min          = minLimit;
//       cardLimit_Max          = maxLimit;
//       cardLimit_Doublication = doublicationLimit;
//    }
//
//    public CardList(CardList cardList) : base(){
//       cardLimit_Min          = cardList.cardLimit_Min;
//       cardLimit_Max          = cardList.cardLimit_Max;
//       cardLimit_Doublication = cardList.cardLimit_Doublication;
//
//       foreach (var card in cardList){
//          base.Add(card);
//       }
//    }
//
//    //********************************************************************************************* Getter & Setters
//    public void setLimits(int minLimit, int maxLimit, int doublicationLimit){
//       cardLimit_Min          = minLimit;
//       cardLimit_Max          = maxLimit;
//       cardLimit_Doublication = doublicationLimit;
//    }
//    
//    public int CardLimitMax{
//       get => cardLimit_Max;
//    }
//
//    public int CardLimitMin{
//       get => cardLimit_Min;
//    }
//
//    public int CardLimitDoublication{
//       get => cardLimit_Doublication;
//    }
//    
//    
//    //********************************************************************************************* Methods
//    //This method is not used because i prefere the possibility to add more Cards, but call the CardList "invalid"
//    // public bool Add(Card card){
//    //    if (base.Count < cardLimit_Max){
//    //       int cardCounter = 0;
//    //       foreach (var deckCard in this){
//    //          if (card == deckCard){
//    //             cardCounter++;
//    //          }
//    //       }
//    //
//    //       if ((cardCounter < cardLimit_Doublication) && (cardCounter < card.getLimited())){
//    //          base.Add(card);
//    //          return true;
//    //       } else{
//    //          return false;
//    //       }
//    //    } else{
//    //       return false;
//    //    }
//    // }
//    
//   
//     
//    
//    public void shuffle(){
//       int currentIndex = base.Count -1;
//       //Random random = new Random();
//       while (currentIndex >= 0) {
//          int rnd = Random.Range(0, base.Count -1);  
//          Card card1 = base[rnd];                // Swap
//          base[rnd] = base[currentIndex];  
//          base[currentIndex] = card1;    
//          currentIndex--;  
//       }
//    }
// }
