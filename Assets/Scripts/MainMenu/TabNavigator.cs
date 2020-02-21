// https://forum.unity.com/threads/tab-between-input-fields-button.568735/

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TabNavigator : MonoBehaviour
{
    public int startIndex = 0;
    private int targetIndex;

    public List<TabObject> tabObjects = new List<TabObject>();
    private EventSystem myEventSystem;


    void Start()
    {
        myEventSystem = EventSystem.current;
        targetIndex = startIndex - 1;
        SetCurrentTabObject();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SetCurrentTabObject();
    }

    void SetCurrentTabObject()
    {
        targetIndex++;
        if (targetIndex >= tabObjects.Count)
            targetIndex = 0;

        if (!tabObjects[targetIndex].tabStop || !tabObjects[targetIndex].tabObject.activeSelf)
        {
            SetCurrentTabObject();
            return;
        }
        myEventSystem.SetSelectedGameObject(tabObjects[targetIndex].tabObject);
    }
}

[System.Serializable]
public class TabObject
{
    public bool tabStop;
    public GameObject tabObject;
}