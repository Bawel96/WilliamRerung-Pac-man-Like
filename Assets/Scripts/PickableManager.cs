using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{

    [SerializeField] private Player player;

    private List<Pickable> pickableList = new List<Pickable>();
    // Start is called before the first frame update
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[]  pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for(int i = 0; i < pickableObjects.Length; i++)
        {
            pickableList.Add(pickableObjects[i]);
            pickableObjects[i].onPicked += OnPickablePicked;
            
        }
        Debug.Log("Pickable list :" + pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        pickableList.Remove(pickable);
        if(pickable.pickableType == PickableType.PowerUP)
        {
            player.PickPowerUp();
        }
        Debug.Log("Pickable list :" + pickableList.Count);
        if (pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }

   
}
