using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour {

   [SerializeField] public GameObject displayBackground;
   [SerializeField] public TMP_Text displayObj;
   [SerializeField] public string displayText;
   [SerializeField] public GameObject areaTemplate;

   [SerializeField] private Creature creature;
   [SerializeField] private Building building;
   [SerializeField] private Terrain terrain;

   private Vector2 position;

   public void initialize(int fontSize, int fieldSize, Vector2 position) {
      this.position = position;
      displayText = position.x + " : " + position.y;

      terrain = this.transform.GetComponentInChildren<Terrain>();

      displayBackground.transform.parent.transform.position += new Vector3(fieldSize, fieldSize) * 0.5f;

      displayBackground.transform.localScale = new Vector3(fieldSize, fieldSize, fieldSize);

      displayObj.text = displayText;
      displayObj.fontSize = fontSize;

      display();
   }

   public void display() {
      //DEBUG: Changes the color of the Debug-Text
      Color textColor = Color.white;
      if (terrain.isBlocking()) {
         //displayBackground.GetComponent<Image>().color = Color.black;
         textColor = new Color(140f/255f, 75f/255f, 38f/255f, 1);
      } else if (!terrain.isAccessible()) {
         //displayBackground.GetComponent<Image>().color = new Color(140f/255f, 75f/255f, 38f/255f, 1); // Brown
         textColor = new Color(140f / 255f, 75f / 255f, 38f / 255f, 1);
      } else {
         if (creature != null && building != null) {
            textColor = new Color(0.31f, 0.05f, 0.71f);
         } else if (creature != null) {
            textColor = Color.red;
         }else if (building != null) {
            textColor = Color.blue;
         }
      }
      displayObj.color = textColor;
      
      //DEBUG: Changes the color of the Debug-Background
      displayBackground.GetComponent<Image>().color = terrain.getTerrainColor();
   }
   
   //************************************************************************************************* highlights
   public void highlight(Color color) {
      displayBackground.GetComponent<Image>().color = color;
   }
   
   //************************************************************************************************* Spawn
   public bool spawnCreature(Creature creature, bool forceSpawn=false){
      if (forceSpawn || checkCompatibility((CreatureCard)creature.getReference())){
         this.creature = creature;
         Debug.Log("Card "+ creature.getReference().name +" got successfully placed at "+ position.x +" "+ position.y);
         display();
         return true;
      }else{
         return false;
      }
   }
   public bool spawnBuilding(Building building, bool forceSpawn=false){
      this.building = building;
      Debug.Log("Card "+ building.getReference().name +" got successfully placed at "+ position.x +" "+ position.y);
      display();
      return true;
   }

   //************************************************************************************************* Private
   private bool checkCompatibility(CreatureCard creatureCard){
      if (hasInfrastructure() && building is Factory){
         Factory factory = (Factory)building;
         return factory.checkCompatibility(creatureCard);
      }else{
         return false;
      }
   }
   
   //************************************************************************************************* Getter & Setters
   public Vector2 getPosition() {
      return position;
   }
   public void setCreature(Creature creature) {
      this.creature = creature;
      display();
   }
   public void setBuilding(Building building) {
      this.building = building;
      display();
   }

   public void setTerrain(TerrainFeature terrain) {
      this.terrain.setTerrain(terrain);
      display();
   }

   public Creature getCreature() {
      return creature;
   }

   public Building getBuilding() {
      return building;
   }
   
   public Terrain getArea() {
      return terrain;
   }

   /// <summary> Checks if there is already a Creature in this Field </summary>
   public bool isOccupied() {
      return (creature != null);
   }

   /// <summary> Checks if there is already a Building in this Field </summary>
   public bool hasInfrastructure() {
      return (building != null);
   }
/*
   /// <summary> Checks if a Creature has the possibility to enter this Field </summary>
   public bool isAccessible() {
      return area.isAccessible();
   }

   /// <summary> Checks if a Building has the possibility to be build in this Field </summary>
   public bool isBuildable() {
      return area.isBuildable();
   }
*/   
   /// <summary> Checks if this Field gives no LineOfSight </summary>
   /// <returns> TRUE if there is NO LoS </returns>
   public bool isBlocking() {
      return terrain.isBlocking();
   }

   /// <summary> Checks if a new Creature can NOW enter this Field </summary>
   /// <returns> TRUE if the field is accessable, it is not a LoS Blocker AND there is not already another Creature in it. </returns>
   public bool isApproachable() {
      return (terrain.isAccessible() && !terrain.isBlocking() && !isOccupied());
   }
   
   /// <summary> Checks if a new Creature can NOW enter this Field </summary>
   /// <returns> TRUE if the field is accessable, it is not a LoS Blocker AND there is not already another Creature in it. </returns>
   public bool hereCanBeBuild() {
      return (terrain.isBuildable() && !terrain.isBlocking()  && !hasInfrastructure());
   }

   //************************************************************************************************* Obsolete
   /// <summary>
   /// Setter for Accessibility-TerrainFeature in Area
   /// </summary>
   /// <param name="blocking"></param>
   /// <returns> False if the Field should be not accessable and has already a creature on it. </returns>
   //public bool makeAccessible(bool accessability) {
   //   if (!accessability && isOccupied()) {
   //      Debug.LogError("Terraforming failed Accessibility" + position);
   //      return false;
   //   } else {
   //      area.makeAccessible(accessability);
   //      display();
   //      return true;
   //   }
   //}

   /// <summary>
   /// Setter for Buildability-TerrainFeature in Area
   /// </summary>
   /// <param name="blocking"></param>
   /// <returns> False if the Field should be not buildable and has already a building on it. </returns>
   //public bool makeBuildable(bool buildability) {
   //   if (!buildability && hasInfrastructure()) {
   //      Debug.LogError("Terraforming failed Buildability"+ position);
   //      return false;
   //   } else {
   //      area.makeBuildable(buildability);
   //      display();
   //      return true;
   //   }
   //}

   /// <summary>
   /// Setter for Blocking-TerrainFeature in Area
   /// </summary>
   /// <param name="blocking"></param>
   /// <returns> False if the Field should be blocking and has already a creature or building on it. </returns>
   //public bool makeBlocker(bool blocking) {
   //   if (blocking && (isOccupied() || hasInfrastructure())) {
   //      Debug.LogError("Terraforming failed Blocking" + position);
   //      return false;
   //   } else {
   //      area.makeBlocker(blocking);
   //      display();
   //      return true;
   //   }
   //}
}
