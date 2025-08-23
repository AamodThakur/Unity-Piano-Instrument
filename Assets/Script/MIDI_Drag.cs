using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MIDI_Drag : MonoBehaviour, IDragHandler
{
    GameObject UI_ToolBar;
    
    private void Start()
    {
        UI_ToolBar = GameObject.FindGameObjectWithTag("UI_ToolBar");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(UI_ToolBar.GetComponent<UI_Button_Function>().DragClicked)
        {
            if (this.GetComponent<RectTransform>().anchoredPosition3D.y >= 0)
            {
                transform.position = new Vector3(transform.position.x,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                    transform.position.z);
            }
 
        }
    }

    void Update()
    {
        if (UI_ToolBar.GetComponent<UI_Button_Function>().DragClicked)
        {
            if (this.GetComponent<RectTransform>().anchoredPosition3D.y < 0)
            {
                this.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                        this.GetComponent<RectTransform>().anchoredPosition3D.x,
                    0,
                    this.GetComponent<RectTransform>().anchoredPosition3D.z);
            }
        }
    }
}
