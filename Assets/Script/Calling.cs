using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触发事件用以调整数据
/// </summary>
public class Calling : MonoBehaviour
{
    public void Nation_Money_Add()  // 国家财富 +1
    {
        Data.Nation_Money += 1;
    }
    public void Self_Money_Add()  // 个人财富 +1
    {
        Data.Self_Money += 1;
    }
    public void Nation_Reputation_Add()  // 国家声誉 +1
    {
        Data.Nation_Reputation += 1;
    }

    public void Wife_Good_Add()
    {
        Data.Wife_Good += 1;
    }
    public void Wife_Good_Sub()
    {
        Data.Wife_Good -= 1;
    }
    
    public void Sheep_Good_Add()    // 羊派群体好感度 +1
    {
        Data.Sheep_Good += 1;
    }
    public void Sheep_Good_Sub()    // 羊派好感度 -1
    {
        Data.Sheep_Good -= 1;
    }
    public void Bird_Good_Add()    // 良禽派群体好感度 +1
    {
        Data.Bird_Good += 1;
    }
    public void Lion_Good_Add()    // 狮派群体好感度 +1
    {
        Data.Lion_Good += 1;
    }

    public void Order_Add()         // 指挥值 +1
    {
        Data.Order += 1;
    }

    public void aebewg_Good_Add()  // 阿尔碧恩王国好感度 +1
    {
        Data.aebewg_Good += 1;
    }
}
