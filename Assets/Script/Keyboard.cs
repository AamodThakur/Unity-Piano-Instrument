using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Keyboard : MonoBehaviour
{
    public GameObject blackkey, whitekey, background_blackkey;
    public GameObject keyboard, background_UI, midiObject;
    GameObject UI_Toolbar;
    public GameObject [] WhiteNotes, BlackNotes;

    public int Octaves;

    // Start is called before the first frame update
    void Start()
    {
        //For Record Button
        UI_Toolbar = GameObject.FindGameObjectWithTag("UI_ToolBar");


        WhiteNotes = new GameObject[Octaves*7];
        BlackNotes = new GameObject[Octaves*5];

        int start_note = 0;

        for (int i = 0; i < Octaves; i++)
        {
            createOctaves(start_note + i * 12, i);
        }
    }

    void createOctaves(int start_note, int octave)
    {
        float keyboard_width = keyboard.GetComponent<RectTransform>().rect.width;
        float width_per_octave = keyboard_width / Octaves;
        float width_per_key = width_per_octave / 7;

        midiObject.GetComponent<MIDI_Handler>().WhiteKeyWidth = width_per_key - 2;
        midiObject.GetComponent<MIDI_Handler>().BlackKeyWidth = width_per_key/2;

        //Create and position white note
        for (int i = 0; i < 7; i++)
        {
            int actualnote = getWhitekeyIndex(i);
            GameObject note = createNote(whitekey, actualnote, start_note);

            //Add White Note to GameObject Array to use in different code
            WhiteNotes[i + octave*7] = note;

            registerevent(note);

            note.GetComponent<RectTransform>().sizeDelta = new Vector2(width_per_key - 2, note.GetComponent<RectTransform>().sizeDelta.y);
            note.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(width_per_octave * octave + width_per_key * i, 0, 0);
        }

        //Create and position black note
        for (int i = 0; i < 5; i++)
        {
            int actualnote = getBlackkeyIndex(i);
            GameObject note = createNote(blackkey, actualnote, start_note);

            //Add White Note to GameObject Array to use in different code
            BlackNotes[i + octave*5] = note;

            registerevent(note);

            note.GetComponent<RectTransform>().sizeDelta = new Vector2(width_per_key/2, note.GetComponent<RectTransform>().sizeDelta.y);

            if (i > 1)
            {
                note.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(width_per_octave * octave + width_per_key * (i+1)  + 2 * width_per_key / 3, keyboard.GetComponent<RectTransform>().sizeDelta.y, 0);
            }
            else
            {
                note.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(width_per_octave * octave + width_per_key * i + 2 * width_per_key / 3, keyboard.GetComponent<RectTransform>().sizeDelta.y, 0);
            }

            if (note.name.Contains("Black"))
            {
                GameObject backgroud = Instantiate(background_blackkey);
                backgroud.transform.SetParent(background_UI.transform, false);
                backgroud.GetComponent<RectTransform>().sizeDelta = new Vector2(note.GetComponent<RectTransform>().rect.width,Screen.height) ;
                backgroud.transform.position = note.transform.position - new Vector3(0, keyboard.GetComponent<RectTransform>().rect.height, 0);
                midiObject.GetComponent<MIDI_Handler>().Black_Background_Arr[octave*5 + i] = note.transform.position.x;
            }
        }
    }

    //Click/Input on Piano Key
    private void registerevent(GameObject note)
    {
        EventTrigger trigger = note.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        pointerDown.eventID = EventTriggerType.PointerDown;
        pointerDown.callback.AddListener((e) => keyOn(note.gameObject));
        trigger.triggers.Add(pointerDown);

        var pointerUp = new EventTrigger.Entry();
        pointerUp.eventID = EventTriggerType.PointerUp;
        pointerUp.callback.AddListener((e) => keyOff(note.gameObject));
        trigger.triggers.Add(pointerUp);
    }

    public void keyOn(GameObject note)
    {
        if (UI_Toolbar.GetComponent<UI_Button_Function>().RecordClicked)
        {
            UI_Toolbar.GetComponent<UI_Button_Function>().first_blood = true;
            UI_Toolbar.GetComponent<UI_Button_Function>().Record_Key(note.name.Contains("White"), note.GetComponent<RectTransform>().anchoredPosition3D.x);
        }
        GameObject.Find("SoundGen").GetComponent<test_oscilattor>().OnKey(note.GetComponent<Piano_Key>().midi_note);
        note.GetComponent<Image>().color = new Color(110f/255f, 189f/255f, 245f/255f);
    }

    public void keyOff(GameObject note)
    {
        GameObject.Find("SoundGen").GetComponent<test_oscilattor>().OffKey(note.GetComponent<Piano_Key>().midi_note);
        if (note.name.Contains("White"))
            note.GetComponent<Image>().color = new Color(1, 1, 1);
        else
            note.GetComponent<Image>().color = new Color(0, 0, 0);
    }

    private GameObject createNote(GameObject note, int actualnote, int start_note)
    {
        GameObject new_note = Instantiate(note);
        new_note.transform.SetParent(keyboard.transform, false);
        new_note.GetComponent<Piano_Key>().midi_note = start_note + actualnote;

        return new_note;
    }

    private int getWhitekeyIndex(int i)
    {
        int actualNote = 0;

        switch(i)
        {
            case 1: actualNote = 2;
                break;
            case 2:
                actualNote = 4;
                break;
            case 3:
                actualNote = 5;
                break;
            case 4:
                actualNote = 7;
                break;
            case 5:
                actualNote = 9;
                break;
            case 6:
                actualNote = 11;
                break;
        }
        return actualNote;
    }

    private int getBlackkeyIndex(int i)
    {
        int actualNote = 1;

        switch (i)
        {
            case 1:
                actualNote = 3;
                break;
            case 2:
                actualNote = 6;
                break;
            case 3:
                actualNote = 8;
                break;
            case 4:
                actualNote = 10;
                break;
        }
        return actualNote;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
