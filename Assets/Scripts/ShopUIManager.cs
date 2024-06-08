using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject furnitureCardPrefab;
    public TMP_Dropdown categoryDropdown;

    void Start()
    {
        InitializeDropdown();
    }

    private void InitializeDropdown()
    {
        List<string> options = new List<string>();
        foreach (FurnitureModel.FurnitureType type in System.Enum.GetValues(typeof(FurnitureModel.FurnitureType)))
        {
            options.Add(type.ToString());
        }

        categoryDropdown.ClearOptions();
        categoryDropdown.AddOptions(options);
        categoryDropdown.onValueChanged.AddListener(OnCategorySelected);

        OnCategorySelected(0);
    }

    private void OnCategorySelected(int index)
    {
        FurnitureModel.FurnitureType selectedType = (FurnitureModel.FurnitureType)index;
        InitializeShopUI(selectedType);
    }

    public void InitializeShopUI(FurnitureModel.FurnitureType selectedType)
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < GameManager.Instance.furnitureModels.Count; i++)
        {
            int index = i;
            var model = GameManager.Instance.furnitureModels[index];
            if (model.type == selectedType)
            {
                GameObject card = Instantiate(furnitureCardPrefab, contentPanel);
                card.transform.Find("ModelImage").GetComponent<Image>().sprite = model.image;
                // card.transform.Find("PriceText").GetComponent<TMP_Text>().text = $"${model.price}";
                
                Button button = card.GetComponent<Button>();
                button.onClick.AddListener(() => OnFurnitureCardClicked(index));
            }
        }
    }

    private void OnFurnitureCardClicked(int index)
    {
        GameManager.Instance.SpawnFurniture(index, GameManager.Instance.playerObject.transform.position);
    }
}
