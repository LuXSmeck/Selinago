using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Building/HQ", fileName = "Building")]
public class HQCard: FactoryCard {
   
   [SerializeField] private List<Type> creatureBufflist;

   //************************************************************************************************* Getter & Setters
   public List<Type> CreatureBufflist => creatureBufflist;
   
}
