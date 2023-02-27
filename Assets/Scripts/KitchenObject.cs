using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   public KitchenObjectSO GetKitchenObject()
    {
        return kitchenObjectSO;
    }
}
