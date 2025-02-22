using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

using System.Text.RegularExpressions;   // 正则表达式（给Regex用的）

[SerializeField]  
public class BeginOption             // 选项
{
    public string text;
    public List<string> action; // 调用什么函数
    public int next_text;       // 下一段文本
}

[SerializeField]    
public class BeginGameData  // 开幕的游戏数据
{
    public int text_id;
    public int year;
    public string text;
    public List<BeginOption> options;
}
public class Beginning : MonoBehaviour
{
    // 给你看看的测试代码 使用LitJson来读，原因是自带的Json解析不能解析嵌套的信息。
    void test() 
    {
        BeginGameData b = new BeginGameData();
        
        b.text_id = 0;
        b.year = 1832;
        b.text = "伴随着第一声啼哭，你来到了这个世界。\n 你来自珂洛国首都的……";
        b.options = new List<BeginOption>();
        
        b.options.Add(new BeginOption(){ text = "1.一个农民家庭", action = new List<string>(){"sheepadd","sheepadd"}, next_text=1});
        b.options.Add(new BeginOption(){ text = "2.一个普通商人家庭", action = new List<string>(){"sheepadd","sheepadd"}, next_text=1});
        b.options.Add(new BeginOption(){ text = "3.一个军人家庭", action = new List<string>(){"sheepadd","sheepadd"}, next_text=1});

        Debug.Log(b.options[0].text);
        Debug.Log(b.options[0].action[0]);
        string str = JsonMapper.ToJson(b);

        str = Regex.Unescape(str); 
        Debug.Log(str);

        BeginGameData c = JsonMapper.ToObject<BeginGameData>(str);
        Debug.Log(c.options[0].text);
        Debug.Log(c.options[0].action[0]);
    }

    public int now_text;    // 当前文本
    public BeginGameData now_data;
    public Dictionary<int, BeginGameData> data = new Dictionary<int, BeginGameData>();  // 文本id的键值对
    void Start()
    {
        now_text = 0;

        // 读Json文件
        Object[] json_File = Resources.LoadAll("beginning",typeof(TextAsset)); // 不需要.json 后缀
        // string json_text = json_File.text;
        for(int i=0; i<json_File.Length;++i)
        {
            TextAsset json_text = (TextAsset)json_File[i];
            BeginGameData b = JsonMapper.ToObject<BeginGameData>(json_text.text);
            data.Add(b.text_id, b);
        }

        Load_Data(now_text);
    }
    

    // 虽然有点乱（但是还好）
    public Text year_text;
    public Text main_text;
    public GameObject select_1;
    public GameObject select_2;
    public GameObject select_3;

    // 加载数据
    void Load_Data(int id)
    {   
        BeginGameData b = data[id];
        now_data = b;

        year_text.text = b.year.ToString();
        main_text.text = b.text;
        
        int len = b.options.Count;
        // SetActive是让该控件显示or not (其实是时候起作用)
        // 可以换个别的方式
        select_1.SetActive(false);
        select_2.SetActive(false);
        select_3.SetActive(false);
        switch(len)
        {
            case 3:
                select_3.GetComponent<Text>().text = b.options[2].text;
                select_3.SetActive(true);
                goto case 2;        // 神tm goto 我服了
            case 2:
                select_2.GetComponent<Text>().text = b.options[1].text;
                select_2.SetActive(true);
                goto case 1;
            case 1:
                select_1.GetComponent<Text>().text = b.options[0].text;
                select_1.SetActive(true);
                break;
        }
    }

    // 用SendMessage非常方便
    public void button_1()
    {
        for(int i = 0; i < now_data.options[0].action.Count; ++i)
        {
            gameObject.SendMessage(now_data.options[0].action[i], SendMessageOptions.DontRequireReceiver);
        }
        now_text = now_data.options[0].next_text;
        Load_Data(now_text);
    }
    public void button_2()
    {
        for(int i = 0; i < now_data.options[1].action.Count; ++i)
        {
            gameObject.SendMessage(now_data.options[1].action[i], SendMessageOptions.DontRequireReceiver);
        }
        now_text = now_data.options[1].next_text;
        Load_Data(now_text);
    }
    public void button_3()
    {
        for(int i = 0; i < now_data.options[2].action.Count; ++i)
        {
            gameObject.SendMessage(now_data.options[2].action[i], SendMessageOptions.DontRequireReceiver);
        }
        now_text = now_data.options[2].next_text;
        Load_Data(now_text);
    }
    
}
