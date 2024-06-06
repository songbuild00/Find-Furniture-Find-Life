using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureShop : MonoBehaviour
{
    public Dropdown categoryDropdown;
    public Transform itemsContainer;
    public GameObject itemPrefab;

    private void Start()
    {
        PopulateCategoryDropdown();
        categoryDropdown.onValueChanged.AddListener(delegate { FilterFurniture(); });
        FilterFurniture();
    }

    private void PopulateCategoryDropdown()
    {
        List<string> options = new List<string>();
        foreach (FurnitureModel.FurnitureType type in Enum.GetValues(typeof(FurnitureModel.FurnitureType)))
        {
            options.Add(type.ToString());
        }
        categoryDropdown.AddOptions(options);
    }

    private void FilterFurniture()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        FurnitureModel.FurnitureType selectedType = (FurnitureModel.FurnitureType)categoryDropdown.value;

        foreach (FurnitureModel furniture in GameManager.Instance.furnitureModels)
        {
            if (furniture.type == selectedType)
            {
                GameObject item = Instantiate(itemPrefab, itemsContainer);
                // item.transform.Find("Image").GetComponent<Image>().sprite = furniture.modelImage;
                item.transform.Find("Price").GetComponent<Text>().text = "$" + furniture.price.ToString("F2");
            }
        }
    }
}
