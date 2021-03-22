using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Call if player try to interact with gameobject.
    /// </summary>
    void OnInteraction();
}
