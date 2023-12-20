using System;
using System.Collections;
using System.Collections.Generic;
using Objects.FieldObjects;
using UnityEngine;

/// <summary>
/// This FieldObject gets created in every single affected Field.
/// RangeToRource defines the Range to the center of an Area and used to manage overlapping Areas
/// </summary>
public class Terrain : AFieldObject {

   [Header("Card Attributes")]
   [SerializeField] private CardSlot cardSlot;
   [SerializeField] private int rangeToSource;

   [Header("TerrainFeatures Attributes")]
   [SerializeField] private TerrainFeature terrainFeature;
   
   private void Awake() {
      initializeStandard();
   }

   public void initialize(CardSlot cardSlot, int rangeToSource, TerrainFeature terrain) {
      this.cardSlot      = cardSlot;
      this.rangeToSource = rangeToSource;
      this.terrainFeature       = terrain;
   }

   public void initializeStandard() {
      initialize(null, 
             100, 
                        CardManager.Instance.standardTerrain);
   }

   /// <summary> resets the CardSlot and the RangeToSource but lets the terrain untouched. </summary>
   public void removeSource(){
      cardSlot = null;
      rangeToSource = 100;
   }
   //************************************************************** private Methods
   //************************************************************************************************* Getter & Setters
   public override PlacableCard getReference() {
      if (cardSlot != null){
         return cardSlot.cardReference;
      }else{
         return null;
      }
   }

   public CardSlot getCardSlot(){
      return cardSlot;
   }

   public int getRange() {
      return rangeToSource;
   }

   public Color getTerrainColor(){
      return terrainFeature.getColor();
   }
   
   /// <summary> Clarifies if a creature can enter this Area </summary>
   public bool isAccessible() {
      return terrainFeature.getFeatures()[0];
   }

   /// <summary> Defines if a Building can be build in this Area </summary>
   public bool isBuildable() {
      return terrainFeature.getFeatures()[1];
   }

   /// <summary> Defines if an Area gives LoS. If it does not, it is automatically also not Accessible or Buildable </summary>
   public bool isBlocking() {
      return terrainFeature.getFeatures()[2];
   }

   /// <summary> WARNING: Only for MAPPING- or DEBUGING-uses
   /// This function reInitialises only the terrain and has no card-relation.
   /// This cannot be undone except by overriting the area with a new terrain! </summary>
   /// <param name="terrain"></param>
   [Obsolete("Prefere to use 'initialize()'")]
   public void setTerrain(TerrainFeature terrain) {
      Debug.LogError("Here found me");
      initialize(null, 100, terrain);
   }
}
