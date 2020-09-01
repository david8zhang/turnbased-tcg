using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{

    [SerializeField]
    Text heatAmountLabel;

    private int _heatAmount;
    private int _totalHeatAmount;

    public int HeatAmount {
        get => _heatAmount;
        set
        {
            _heatAmount = value;
        }
    }
    public int TotalHeatAmount {
        get => _totalHeatAmount;
        set {
            _totalHeatAmount = value;
        }
    }


    public void SubtractHeat(int amount)
    {
        HeatAmount -= amount;
        RenderHeatAmount();
    }

    public void ResetHeat()
    {
        HeatAmount = TotalHeatAmount;
        RenderHeatAmount();
    }

    public void RenderHeatAmount()
    {
        heatAmountLabel.text = HeatAmount.ToString() + "/" + TotalHeatAmount.ToString();
    }
}
