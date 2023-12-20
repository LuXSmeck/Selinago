using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestingArea : MonoBehaviour {

   [Header("Cards")]
   [SerializeField] private PlacableCard[] cards;
   [SerializeField] private CreatureCard[] myMonsters;

   [Header("Weapons")]
   [SerializeField] private WeaponCard myWeapon;

   [Header("Terrains")]
   [SerializeField] private AreaCard[] terrainCards;

   [Header("PathFinding")]
   [SerializeField] private int myCardSlotId = 0;
   [SerializeField] private int targetCardSlotId = 0;
   [SerializeField] private int targetX = 2;
   [SerializeField] private int targetY = 2;

   CardManager cardManager;

   // Start is called before the first frame update
   void Start() {
      cardManager = CardManager.Instance;
      Debug.LogAssertion("***** Testing Start ***** ");
      test();
   }

   [ContextMenu("RunTest_ALL")]
   public void test() {
      testPlacement();
      testTerraforming();
      testSelect();
   }
   
   public void testPlacement() {
      Debug.LogAssertion("***** Creature Placing: RED Labels ***** ");
      cardManager.cardSlots[0].placeCard(myMonsters[0], 1, 1);
      cardManager.cardSlots[1].placeCard(myMonsters[1], 1, 2);
      cardManager.cardSlots[2].placeCard(myMonsters[2], 4, 4);
      cardManager.cardSlots[3].placeCard(myMonsters[0], 2, 1);
   }

   [ContextMenu("ErrorTest_Placement")]
   private void testErrorPlacement(){
      Debug.LogAssertion("***** Creature Placing: Errors inc? ");
      Debug.Log("Same Slot");
      cardManager.cardSlots[1].placeCard(myMonsters[3], 4, 1);
      cardManager.cardSlots[2].placeCard(myMonsters[3], 4, 2);
      Debug.Log("Same Field");
      cardManager.cardSlots[4].placeCard(myMonsters[4], 1, 2);
      cardManager.cardSlots[5].placeCard(myMonsters[4], 4, 4);
   }

   public void testTerraforming() {
      Debug.LogAssertion("***** Terrain ***** ");
      cardManager.cardSlots[6].placeCard(terrainCards[0], 4, 4);
      cardManager.cardSlots[7].placeCard(terrainCards[2], 3, 6);
   }

   [ContextMenu("ErrorTest_Terraforming")]
   private void testErrorTerraformingt(){
      Debug.Log("provoked Errors start *****");
      cardManager.cardSlots[8].placeCard(terrainCards[3], 2, 2);
      cardManager.cardSlots[9].placeCard(terrainCards[3], 2, 0);
   }

   [ContextMenu("RunTest_Select()")]
   public void testSelect() {
      Debug.LogAssertion("***** Select ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId]);
   }
   
   [ContextMenu("RunTest_PathFinding")]
   public void testGridPathFind() {
      Debug.LogAssertion("***** PathFind ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId], true);
      cardManager.tryMoveabilityCreature(targetX, targetY);
   }
   
   [ContextMenu("RunTest_Movement")]
   public void testGridMovement() {
      Debug.LogAssertion("***** Movement ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId], true);
      int result = cardManager.tryMoveabilityCreature(targetX, targetY);
      switch (result) {
         case 0: Debug.Log("Movement is possible");
                 cardManager.confirmMovement(targetX, targetY);
                 break;
         case 1: Debug.Log("Movement is not possible: Target is out of Range");
                 break;
         default: Debug.Log("Movement is not possible);");
                 break;
      }
   }
   
   [ContextMenu("RunTest_Fighting")]
   public void testFighting() {
      Debug.LogAssertion("***** Fight ***** ");
      cardManager.setSelectedField(cardManager.cardSlots[myCardSlotId]);
      Field defendingField = cardManager.cardSlots[targetCardSlotId].fieldReference;
      int result = cardManager.tryAttackingCreatureAt(defendingField);
      switch (result) {
         case 0: Debug.Log("Fight is possible");
            cardManager.confirmAttack(defendingField.getCreature());
            break;
         case 1: Debug.Log("Fight is not possible: Target is out of Range");
            break;
         default: Debug.Log("Fight is not possible);");
            break;
      }
   }

   
   [ContextMenu("RunTest_Unselect")]
   public void testGridUnselect() {
      Debug.LogAssertion("***** Unselect ***** ");
      cardManager.cancelAction();
   }


   
}
