/* It will trigger play button. Falling notes will generate sound when they reach piano*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIDI_Play : MonoBehaviour
{
    GameObject UI_ToolBar;
    float speed;
    int Octaves, midi_no;
    GameObject cam;
    private bool flag = true;

    private void Awake()
    {
        UI_ToolBar = GameObject.FindGameObjectWithTag("UI_ToolBar");
        cam = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        Octaves = cam.GetComponent<Keyboard>().Octaves;
        speed = cam.GetComponent<Setting_Controller>().Midi_Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UI_ToolBar.GetComponent<UI_Button_Function>().Play_Clicked)
        {
            this.gameObject.transform.position += Vector3.down * speed * Time.deltaTime;

            if(this.GetComponent<RectTransform>().anchoredPosition3D.y <= 0 && flag)
            {
                flag = false;   //Generate sound then delete
                if (this.name.Contains("WhiteMIDI"))
                {
                    GameObject[] WhiteNotes = cam.GetComponent<Keyboard>().WhiteNotes;
                    int i = 0;
                    while (this.GetComponent<RectTransform>().anchoredPosition3D.x - 0.1 > WhiteNotes[i].GetComponent<RectTransform>().anchoredPosition3D.x)
                    {
                        i++;
                    }
                    midi_no = i;


                    //midi_no = Mathf.FloorToInt((this.GetComponent<RectTransform>().anchoredPosition3D.x - 0.1f) / this.GetComponent<RectTransform>().sizeDelta.x);
                    cam.GetComponent<Keyboard>().keyOn(cam.GetComponent<Keyboard>().WhiteNotes[midi_no]);
                }
                else
                {
                    //Black Notes
                    GameObject [] BlackNotes = cam.GetComponent<Keyboard>().BlackNotes;
                    int i = 0;
                    while (this.GetComponent<RectTransform>().anchoredPosition3D.x - 0.1 > BlackNotes[i].GetComponent<RectTransform>().anchoredPosition3D.x)
                    {
                        i++;
                    }
                    midi_no = i;
                    cam.GetComponent<Keyboard>().keyOn(cam.GetComponent<Keyboard>().BlackNotes[midi_no]);
                }
            }

            //Delete note if touched piano & turn light off when notes fully behind piano.
            if(this.GetComponent<RectTransform>().anchoredPosition3D.y <= -this.GetComponent<RectTransform>().sizeDelta.y && !flag)
            {
                if (this.name.Contains("WhiteMIDI"))
                    cam.GetComponent<Keyboard>().keyOff(cam.GetComponent<Keyboard>().WhiteNotes[midi_no]);
                else
                    cam.GetComponent<Keyboard>().keyOff(cam.GetComponent<Keyboard>().BlackNotes[midi_no]);
                Destroy(this.gameObject);
            } 
        }
    }
}
