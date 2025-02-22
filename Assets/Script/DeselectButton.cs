using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectButton : MonoBehaviour
{
    public void Deselect()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}