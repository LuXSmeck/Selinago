using UnityEngine;

namespace Objects.FieldObjects{
   public abstract class AFieldObject : MonoBehaviour {

      [Header("Field Attributes")]
      [SerializeField] private int fieldReferenceX;
      [SerializeField] private int fieldReferenceY;

      public abstract PlacableCard getReference();

      public void setPosition(int positionX, int positionY) {
         fieldReferenceX = positionX;
         fieldReferenceY = positionY;
      }
   }
}
