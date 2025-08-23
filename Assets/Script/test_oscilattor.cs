using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_oscilattor : MonoBehaviour
{
    public float frequency = 440;

    //private double ;
    private float phase, increament, sampling_freq = 48000;

    public float gain;
    private float time=0, sustain_temp = 1f;
    public float on = 1f, off = 1.3f;
    public bool sustain = false;

    private void Update()
    {

    }

    public void OnKey(int midi)
    {
        frequency = Mathf.Pow(2, (float)midi / 12) * 130.82f;
        gain = 0.1f;
        time = 0;
        sustain_temp = on;
    }

    public void OffKey(int midi)
    {
        if(sustain)
            sustain_temp = off;
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        increament = frequency * 2 * ((float)(Mathf.PI)) / sampling_freq;

        for(int i = 0; i < data.Length; i += channels)
        {
            time += increament;

            data[i] =  0.6f * Mathf.Sin(1*time) * Mathf.Exp((-0.0007f) * time * sustain_temp);
            data[i] += 0.4f * Mathf.Sin(2*time) * Mathf.Exp((-0.0007f) * time * sustain_temp);
            data[i] += Mathf.Pow(data[i], 3);
            data[i] *= 1 + 16 * time * Mathf.Exp(-6 * time);

            data[i] = gain * data[i];

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

        }
    }
}
