using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldObject : MonoBehaviour {

   [Header("Field Attributes")]
   [SerializeField] private int fieldReferenceX;
   [SerializeField] private int fieldReferenceY;

   public abstract PlacableCard getReference();

   public void setPosition(int positionX, int positionY) {
      fieldReferenceX = positionX;
      fieldReferenceY = positionY;
   }
}
