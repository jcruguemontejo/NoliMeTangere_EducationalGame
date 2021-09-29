using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask interactablesLayer;
    [SerializeField] LayerMask playersLayer;
    [SerializeField] LayerMask fovsLayer;
    [SerializeField] LayerMask portalsLayer;

    public static GameLayers i { get; set; }

    public void Awake()
    {
        i = this;
    }

    public LayerMask solidLayer
    {
        get => solidObjectsLayer;
    }

    public LayerMask interactableLayer
    {
        get => interactablesLayer;
    }

    public LayerMask playerLayer
    {
        get => playersLayer;
    }

    public LayerMask fovLayer
    {
        get => fovsLayer;
    }

    public LayerMask portalLayer
    {
        get => portalsLayer;
    }

    public LayerMask triggerableLayers 
    {
        get => fovLayer | portalLayer;
    }
}
