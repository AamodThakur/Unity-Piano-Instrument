using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_MIDI : MonoBehaviour
{
    GameObject UI_Toolbar;

    public void DeleteMIDI()
    {
        UI_Toolbar = GameObject.FindGameObjectWithTag("UI_ToolBar");

        if (UI_Toolbar.GetComponent<UI_Button_Function>().DeleteClicked)
        {
            Destroy(this.gameObject);
        }
    }
}
