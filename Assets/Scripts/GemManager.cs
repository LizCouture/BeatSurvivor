using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : SingletonMonobehaviour <GemManager>
{
    private List<GameObject> gemsInScene = new List<GameObject>();

    public List<GameObject> GemsInScene { get => gemsInScene; set => gemsInScene = value; }

    public void AddGem(GameObject gem) {
        gemsInScene.Add(gem);
    }

    public void RemoveGem(GameObject gem) {
        gemsInScene.Remove(gem);
    }
}
