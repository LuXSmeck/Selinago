using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject{

   private static int idCounter;
    
   [Header("Generel Attributes")]
   [SerializeField] protected int id;
   [SerializeField] protected string cardName;
   [SerializeField] protected string cardText;
   [SerializeField] protected int level;
   [SerializeField] protected int limited;
   protected int costs;

   public Card(){
      id = ++idCounter;
      costs = level;
      limited = int.MaxValue;
   }

   public int getID(){
      return id;
   }

   public string getName(){
      return cardName;
   }

   public int getLevel(){
      return level;
   }

   public int getCosts(){
      return costs;
   }

   /// <summary>
   /// Returns the limited value of this card.
   /// A Deck can not include more doubles of this card than this number indicates.
   /// If the given value is "MaxValue", the card is not limited.
   /// </summary>
   /// <returns></returns>
   public int getLimited(){
      return limited;
   }

   public void setLimited(int limit){
      limited = limit;
   }
}
