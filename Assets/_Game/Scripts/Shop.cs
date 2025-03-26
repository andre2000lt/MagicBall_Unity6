using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button _noAdsButton;


    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "magicball_remove_ads":
                DisableAds();
                break;
        }
    }


    private void DisableAds()
    {
        PlayerPrefs.SetInt("noAds", 1);
        _noAdsButton.gameObject.SetActive(false);
    }
}
