using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enums/Terrain", fileName = "Terrain")]
/// <summary>
/// Determines an Terrain and its features f.ex.:
/// "Planes"; "Street"; "Mountains"...
/// </summary>
public class TerrainFeature : ScriptableObject {

   [SerializeField] private string terrainName;
   [SerializeField] private Color color;

   [Header("TerrainFeatures Attributes")]
   [Tooltip("Defines if a Creature can access the Area.")]
   [SerializeField] private bool accessibility;
   [Tooltip("Defines if a Building can be placed in the Area.")]
   [SerializeField] private bool buildability;
   [Tooltip("Defines the Area is lineOfSight-Blocker.")]
   [SerializeField] private bool blocking;
   
   public string getName() {
      return terrainName;
   }
   
   public Color getColor(){
      return color;
   }
   
   /// <returns> Array of the Attributes "accessibility; buildabiltiy and blocking" </returns>
   public bool[] getFeatures() {
      bool[] result = {accessibility, buildability, blocking};
      return result;
   }
}
