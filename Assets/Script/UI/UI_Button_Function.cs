using UnityEngine;
using System.Collections.Generic;

public class UI_Button_Function : MonoBehaviour
{
    public GameObject midiObject;
    public bool DrawClicked = false, DragClicked = false, DeleteClicked = false, Play_Clicked = false;
    public bool RecordClicked = false, first_blood = false;
    List<Vector3> WhiteMidiPos = new List<Vector3>(), BlackMidiPos = new List<Vector3>();
    GameObject MIDI_Controller, MIDIWhite, MIDIBlack, cam;
    float Rpos, speed;

    private void Start()
    {
        MIDI_Controller = GameObject.FindGameObjectWithTag("Midi_Parent");
        cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        speed = cam.GetComponent<Setting_Controller>().Midi_Speed;
    }

    public void Record_Key(bool isWhite, float pos)
    {
        if(isWhite)
        {
            WhiteMidiPos.Add(new Vector3(pos, Rpos, 0));
        }
        else
        {
            BlackMidiPos.Add(new Vector3(pos, Rpos, 0));
        }    
    }

    public void Record_Button()
    {
        if (Play_Clicked)
            return;
        DrawClicked = DragClicked = DeleteClicked = false;

        if (!RecordClicked)
        {
            first_blood = false;
            for(int i = 0; i < MIDI_Controller.transform.childCount; i++)
            {
                Destroy(MIDI_Controller.transform.GetChild(i).gameObject);
            }
            Rpos = 10;      //Offset
            WhiteMidiPos = new List<Vector3>();
            BlackMidiPos = new List<Vector3>();
        }
        else
        {
            //Debug.Log(WhiteMidiPos[0]);
            while (WhiteMidiPos.Count != 0)
            {
                GameObject midiNoteTemp = Instantiate(MIDI_Controller.GetComponent<MIDI_Handler>().MIDIWhite);
                midiNoteTemp.transform.SetParent(MIDI_Controller.transform);
                midiNoteTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(MIDI_Controller.GetComponent<MIDI_Handler>().WhiteKeyWidth, midiNoteTemp.GetComponent<RectTransform>().sizeDelta.y);
                midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D = WhiteMidiPos[0];
                WhiteMidiPos.RemoveAt(0);
            }
            while (BlackMidiPos.Count != 0)
            {
                GameObject midiNoteTemp = Instantiate(MIDI_Controller.GetComponent<MIDI_Handler>().MIDIBlack);
                midiNoteTemp.transform.SetParent(MIDI_Controller.transform);
                midiNoteTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(MIDI_Controller.GetComponent<MIDI_Handler>().BlackKeyWidth, midiNoteTemp.GetComponent<RectTransform>().sizeDelta.y);
                midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D = BlackMidiPos[0];
                BlackMidiPos.RemoveAt(0);
            }
        }
        RecordClicked = !RecordClicked;
    }

    public void Draw_Button()
    {
        if (Play_Clicked)
            return;
        DragClicked = DeleteClicked = Play_Clicked = false;
        DrawClicked = !DrawClicked;
    }

    public void Drag_Button()
    {
        if (Play_Clicked)
            return;
        DrawClicked = DeleteClicked = Play_Clicked = false;
        DragClicked = !DragClicked;
    }

    public void Delete_Button()
    {
        if (Play_Clicked)
            return;
        DragClicked = DrawClicked = Play_Clicked = false;
        DeleteClicked = !DeleteClicked;
    }

    public void Play_Button()
    {
        DragClicked = DrawClicked = DeleteClicked = false;

        if(!Play_Clicked)
        {
            //Play_Clicked is false but it is going to be true
            //Save the notes

            //Don't kill just reposition it
            WhiteMidiPos = new List<Vector3>();
            BlackMidiPos = new List<Vector3>();
            int count = MIDI_Controller.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                if (MIDI_Controller.transform.GetChild(i).name.Contains("White"))
                {
                    WhiteMidiPos.Add(MIDI_Controller.transform.GetChild(i).gameObject.GetComponent<RectTransform>().anchoredPosition3D);
                }
                else
                {
                    BlackMidiPos.Add(MIDI_Controller.transform.GetChild(i).gameObject.GetComponent<RectTransform>().anchoredPosition3D);
                }
            }
        }
        else
        {
            //Load notes
            int count = MIDI_Controller.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(MIDI_Controller.transform.GetChild(i).gameObject);
            }

            while (WhiteMidiPos.Count != 0)
            {
                GameObject midiNoteTemp = Instantiate(MIDI_Controller.GetComponent<MIDI_Handler>().MIDIWhite);
                midiNoteTemp.transform.SetParent(MIDI_Controller.transform);
                midiNoteTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(MIDI_Controller.GetComponent<MIDI_Handler>().WhiteKeyWidth, midiNoteTemp.GetComponent<RectTransform>().sizeDelta.y);
                midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D = WhiteMidiPos[0];
                WhiteMidiPos.RemoveAt(0);
            }
            while (BlackMidiPos.Count != 0)
            {
                GameObject midiNoteTemp = Instantiate(MIDI_Controller.GetComponent<MIDI_Handler>().MIDIBlack);
                midiNoteTemp.transform.SetParent(MIDI_Controller.transform);
                midiNoteTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(MIDI_Controller.GetComponent<MIDI_Handler>().BlackKeyWidth, midiNoteTemp.GetComponent<RectTransform>().sizeDelta.y);
                midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D = BlackMidiPos[0];
                BlackMidiPos.RemoveAt(0);
            }
        }

        Play_Clicked = !Play_Clicked;
    }

    public void Update()
    {
        if(RecordClicked && first_blood)
        {
            Rpos += speed * Time.deltaTime;
        }
    }
}
