using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<IMyComponent> Components;

    private void Start()
    {
        Components = new List<IMyComponent>();
        
        Components.Add(new InputComponet());
        Components.Add(new JumpComponet());
        Components.Add(new AttackComponet());
    }

    private void Update()
    {
        foreach (var Component in Components)
        {
            Component.UpdateMyComponent();
        }
    }

    public void SendMessageMyComponent()
    {
        foreach (var Component in Components)
        {
            Component.RecevieMessage();
        }
    }
}
