using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

[SerializeField]  
public class ChatItem
{
    public string name;     // 谁说的话 设置旁白为os，进行特殊处理
    public string text;     // 说了啥
}

[SerializeField]
public class IncidentOption
{
    public string text;
    public List<ChatItem> chats;
    public List<string> action;
    public int next_text;
}

[SerializeField]
public class IncidentGameData
{
    public int text_id;
    public List<ChatItem> chats;
    public List<IncidentOption> options;
}

/// <summary>
/// 事件相关代码
/// </summary>
public class Incident : MonoBehaviour
{
    public Transform Content;

    // 预制体组件，即框内的文本内容
    public GameObject os_prefab;
    public GameObject chat_prefab;
    public GameObject button_prefab;

    public int now_text;    // 当前文本
    public IncidentGameData now_data;
    public Dictionary<int, IncidentGameData> data; // 文本id的键值对
    public Dictionary<string, IncidentOption> options = new Dictionary<string, IncidentOption>();
    public Dictionary<string, GameObject> buttons = new Dictionary<string, GameObject>();

    void Start()
    {
        Load_Incident("incident2");
    }

    // 加载事件
    public void Load_Incident(string Incident_name)
    {
        now_text = 0;
        data = new Dictionary<int, IncidentGameData>();
        clean_all();

        // 读Json文件
        Object[] json_File = Resources.LoadAll(Incident_name,typeof(TextAsset)); // 不需要.json 后缀
        // string json_text = json_File.text;
        for(int i=0; i<json_File.Length;++i)
        {
            TextAsset json_text = (TextAsset)json_File[i];
            IncidentGameData b = JsonMapper.ToObject<IncidentGameData>(json_text.text);
            data.Add(b.text_id, b);
        }

        Load_Data(now_text);
    }

    void Load_Data(int id)
    {
        IncidentGameData d = data[id];
        now_data = d;

        // 生成文本框（chats)
        for(int i=0; i < d.chats.Count; ++i)
        {
            if(d.chats[i].name == "os")
            {
                GameObject obj = (GameObject)Instantiate(os_prefab, Content);  // 创建游戏物体（文本框）
                obj.GetComponent<Text>().text = d.chats[i].text;
            }
            else
            {
                GameObject obj = (GameObject)Instantiate(chat_prefab, Content);  // 创建游戏物体（文本框）
                obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = d.chats[i].name + "：";
                obj.transform.GetChild(1).gameObject.GetComponent<Text>().text = d.chats[i].text;
            }
        }

        // 生成选项按钮
        for(int i=0; i<d.options.Count; ++i)
        {
            options.Add(d.options[i].text, d.options[i]);
        }

        clear_all_button();
        new_button();
    }

    // 清空所有选项
    void clear_all_button()
    {
        foreach (string key in buttons.Keys)
        {
            Destroy(buttons[key]);
        }
        buttons.Clear();
    }    
    // 增添选项
    void new_button()
    {
        int num = 1;
        foreach (string key in options.Keys)
        {
            IncidentOption opt = options[key];
            GameObject obj = (GameObject)Instantiate(button_prefab, Content);
            obj.GetComponent<Text>().text = num.ToString() + ". " + opt.text;
            buttons.Add(opt.text, obj);

            ++num;
        }

        Invoke("Refresh", Time.deltaTime * 10);  // 延迟执行，等待10帧时间   
    }

    // 清除Content中的全部内容
    void clean_all()
    {

    }

    // 移动到最下面
    void Refresh()
    {
        // 获取对话框高度
        float h = Content.gameObject.GetComponent<RectTransform>().rect.height;

        Vector3 pos = Content.position;//pos并非引用。
        pos.y = h;
        Content.position = pos;
    }

    public void Incident_Button(string key)
    {
        IncidentOption opt = options[key];

        for(int i = 0; i < opt.action.Count; ++i)
        {
            gameObject.SendMessage(opt.action[i], SendMessageOptions.DontRequireReceiver);
        }
        
        clear_all_button();

        int next_id = opt.next_text;
        if(next_id == -1)   // 不跳转下一个，就直接说话
        {
            options.Remove(key);
            foreach (string k in options.Keys)
            {
                Debug.Log(k);
            }
        }
        else if(next_id == -2)
        {
            Debug.Log("结束该事件");
        }
        else
        {
            options.Clear();
        }

        for(int i=0; i < opt.chats.Count; ++i)
        {
            if(opt.chats[i].name == "os")
            {
                GameObject obj = (GameObject)Instantiate(os_prefab, Content);  // 创建游戏物体（文本框）
                obj.GetComponent<Text>().text = opt.chats[i].text;
            }
            else
            {
                GameObject obj = (GameObject)Instantiate(chat_prefab, Content);  // 创建游戏物体（文本框）
                obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = opt.chats[i].name + "：";
                obj.transform.GetChild(1).gameObject.GetComponent<Text>().text = opt.chats[i].text;
            }
        }

        if(next_id >= 0)
        {
            now_text = next_id;
            Load_Data(now_text);
        }
        else if(next_id == -1)
        {
            new_button();
        }
        
        
    }
}
