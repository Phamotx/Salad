using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer: MonoBehaviour
{
    [SerializeField]
    private string vegType;

    public string VegType { get => vegType; set => vegType = value; }
}
