using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public Image[] stars;

    public void UpdateStars(float score)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < score)
            {
                stars[i].enabled = true;
                stars[i].fillAmount = score - i < 1 ? score - i : 1;
            }
            else
            {
                stars[i].enabled = false;
            }
        }
    }
}
