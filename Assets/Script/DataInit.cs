using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInit : MonoBehaviour
{
    // 可以写读存档等
    void Start()
    {
        Data.Nation_Money = 100;
        Data.Self_Money = 100;
        Data.Nation_Reputation = 0;

        Data.Wife_Good = 0;
        Data.Order = 0;
        Data.Lion_Good = 5;
        Data.Bird_Good = 5;
        Data.Sheep_Good = 5;
        
        Data.aebewg_Good = 0;
    }
}
