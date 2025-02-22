using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncidentButton : MonoBehaviour
{
    public void Button_Call()
    {
        GameObject.Find("Script").GetComponent<Incident>().Incident_Button(gameObject.GetComponent<Text>().text.Substring(3));
    }
}
