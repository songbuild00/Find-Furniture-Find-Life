using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class FurnitureModel
{
    public FurnitureType type;
    public GameObject model;
    public string name;
    public Vector3 center;
    public Vector3 size;
    public int price;

    public FurnitureModel(GameObject model, string name, Vector3 center, Vector3 size, int price)
    {
        this.model = model;
        this.name = name;
        this.center = center;
        this.size = size;
        this.price = price;
    }

    public void InstantiateSetting(GameObject gameObject, GameObject playerObject)
    {
        Moveable moveable = gameObject.AddComponent<Moveable>();
        moveable.player = playerObject;

        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();

        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = center;
        boxCollider.size = size;

        XRSimpleInteractable simpleInteractable = gameObject.AddComponent<XRSimpleInteractable>();
        simpleInteractable.selectEntered.AddListener((args) => OnSelectEntered(args, moveable));
        simpleInteractable.selectExited.AddListener((args) => OnSelectExited(args, moveable));

        gameObject.tag = "Furniture-" + type.ToString();
        Debug.Log("Spawned!");
    }

    private void OnSelectEntered(SelectEnterEventArgs args, Moveable moveable)
    {
        if (args.interactorObject.transform.name.Contains("Left"))
        {
            moveable.StartPushing();
        }
        else if (args.interactorObject.transform.name.Contains("Right")) 
        {
            moveable.StartRotating();
        }
    }

    private void OnSelectExited(SelectExitEventArgs args, Moveable moveable)
    {
        if (args.interactorObject.transform.name.Contains("Left"))
        {
            moveable.StopPushing();
        }
        else if (args.interactorObject.transform.name.Contains("Right")) 
        {
            moveable.StopRotating();
        }
    }

    public enum FurnitureType
    {
        Bed, Chair, Table, Wardrobe, Lamp, Frame
    }
}
