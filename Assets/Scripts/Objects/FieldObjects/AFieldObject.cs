using UnityEngine;

namespace Objects.FieldObjects{
   public abstract class AFieldObject : MonoBehaviour {
      
      [SerializeField] private int energyGeneration = 0;

      public int EnergyGeneration => energyGeneration;

      public abstract PlacableCard getReference();
      
      
      /// <summary>
      /// initialise the sequence of destroying the Object; removing the Card from the Field;
      /// deleting all references.
      /// </summary>
      public virtual void iniDestroySequence(){
         //nothing yet
      }

   }
}
