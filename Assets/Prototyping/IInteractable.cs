using UnityEngine;
using System.Collections;

public interface IInteractable
{
    void Interact(EntityBase interactingEntity = null);
}
