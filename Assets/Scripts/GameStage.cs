using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class GameStage
{
    public string stageName;
    public List<Condition> conditions;

    public string nextStage;
    public string prevStage;

    public bool CheckAllConditions() {
        GameObject[] furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (var furniture in furnitures) {
            
        }
        return false;
    }

    [System.Serializable]
    public class Condition
    {
        public ConditionType type;
        public List<GameObject> objects;
        public double value;
    }

    public enum ConditionType
    {
        NEARBY
    }
}
