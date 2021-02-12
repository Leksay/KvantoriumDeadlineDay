using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGlobalActivator
{
    List<IActivatable> Activables { get; set; }
    void ActivateAll();
    void DeactivateAll();
}
