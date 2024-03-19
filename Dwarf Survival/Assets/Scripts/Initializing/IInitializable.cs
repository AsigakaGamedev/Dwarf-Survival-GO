using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitializable 
{
    public abstract void OnInitialize();
    public abstract void OnDeinitialize();
}
