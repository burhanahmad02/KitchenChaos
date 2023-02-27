using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private KitchenObjectSO kitchenObject;
    public void Interact()
    {
        Debug.Log("Interact!");
        Transform kitchenObjectTransform = Instantiate(kitchenObject.prefab, spawnPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
        Debug.Log(kitchenObjectTransform.transform.GetComponent<KitchenObject>().GetKitchenObject().objectName);
    }
}
