using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[Serializable]
public class GameStage
{
    public string stageName;
    public List<Condition> conditions;
    [TextArea(1, 10)]
    public string conditionText;

    public string nextStage;
    public string prevStage;

    public float CheckAllConditions() {
        float score = 1.0f;
        foreach (var condition in conditions)
        {
            if (!condition.Check()) {
                score -= 1.0f / conditions.Count;
            }
        }
        return score;
    }

    [Serializable]
    public class Condition
    {
        public ConditionType type;
        public List<FurnitureModel.FurnitureType> furnitureTypes;
        public double valueD;
        public string valueS;

        public bool Check()
        {
            if (type == ConditionType.NEARBY)
            {
                for (int i = 0; i < furnitureTypes.Count; i++)
                {
                    for (int j = i + 1; j < furnitureTypes.Count; j++)
                    {
                        GameObject furniture1 = GameObject.FindGameObjectWithTag("Furniture-" + furnitureTypes[i].ToString());
                        GameObject furniture2 = GameObject.FindGameObjectWithTag("Furniture-" + furnitureTypes[j].ToString());
                        if (furniture1 == null || furniture2 == null) return false;
                        if (Vector3.Distance(furniture1.transform.position, furniture2.transform.position) > valueD) return false;
                    }
                }
                return true;
            }
            else if (type == ConditionType.COLOR)
            {
                for (int i = 0; i < furnitureTypes.Count; i++) 
                {
                    GameObject furniture = GameObject.FindGameObjectWithTag("Furniture-" + furnitureTypes[i].ToString());
                    if (furniture == null) return false;
                    if (furniture.GetComponent<Colored>().color != valueS) return false;
                }
            }
            return false;
        }
    }

    public enum ConditionType
    {
        NEARBY, COLOR
    }
}
