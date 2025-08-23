using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MIDI_Handler : MonoBehaviour
{
    public float WhiteKeyWidth, BlackKeyWidth;
    public GameObject MIDIWhite, MIDIBlack;
    private int Octaves;
    private Vector3 mouse_pos;
    public float[] Black_Background_Arr;
    private bool MIDI_Flag = true;
    private GameObject midiNoteTemp, UI_ToolBar;


    private void Awake()
    {
        UI_ToolBar = GameObject.FindGameObjectWithTag("UI_ToolBar");
        GameObject cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        Octaves = cam.GetComponent<Keyboard>().Octaves;
        Black_Background_Arr = new float[5 * Octaves];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Chekk if mouse over UI
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (UI_ToolBar.GetComponent<UI_Button_Function>().DrawClicked && Input.GetMouseButtonDown(0))
        {
            mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MIDI_Flag = true;


            //Used to draw Black key MIDI
            for (int i = 0; i < Octaves * 5; i++)
            {
                if (mouse_pos.x > Black_Background_Arr[i] && mouse_pos.x < Black_Background_Arr[i]+BlackKeyWidth)
                {
                    MIDI_Flag = false;
                    midiNoteTemp = Instantiate(MIDIBlack);
                    midiNoteTemp.transform.SetParent(this.transform);
                    midiNoteTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(BlackKeyWidth, midiNoteTemp.GetComponent<RectTransform>().sizeDelta.y);
                    midiNoteTemp.transform.position = new Vector3(Black_Background_Arr[i], mouse_pos.y, 0);
                    //midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(Black_Background_Arr[i], midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D.y, 0);
                    break;
                }
            }

            if (MIDI_Flag)
            {
                midiNoteTemp = Instantiate(MIDIWhite);
                midiNoteTemp.transform.SetParent(this.transform);
                midiNoteTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(WhiteKeyWidth, midiNoteTemp.GetComponent<RectTransform>().sizeDelta.y);
                midiNoteTemp.transform.position = new Vector3(mouse_pos.x, mouse_pos.y, 0);
                //Used to draw white key MIDI
                /*Brinary search can be used*/
                for (int i = 0; i <= Octaves * 7; i++)
                {
                    if ((midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D.x) < i * (WhiteKeyWidth + 2))
                    {
                        midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D = new Vector3((i - 1) * (WhiteKeyWidth + 2), midiNoteTemp.GetComponent<RectTransform>().anchoredPosition3D.y, 0);
                        break;
                    }
                }
            }
        }
    }
}
