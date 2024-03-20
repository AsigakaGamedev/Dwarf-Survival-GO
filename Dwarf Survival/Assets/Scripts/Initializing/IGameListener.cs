using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameListener
{

}

public interface IInitListener : IGameListener
{
    public abstract void OnInitialize();
}

public interface IDeinitListener : IGameListener
{
    public abstract void OnDeinitialize();
}

public interface IUpdateListener : IGameListener
{
    public abstract void OnUpdate();
}
