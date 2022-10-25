using System;
using TMPro;
using UnityEngine;


public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerEnergyText;
    
    public TMP_Text PlayerEnergy => playerEnergyText;
}