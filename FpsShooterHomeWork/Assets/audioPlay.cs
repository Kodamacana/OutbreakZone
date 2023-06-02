using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlay : MonoBehaviour
{
    AudioSource aud;

    [SerializeField]
    AudioClip table1;

    [SerializeField]
    AudioClip table2;

    [SerializeField]
    AudioClip table3;

    [SerializeField]
    AudioClip table4;

    [SerializeField]
    AudioClip table5;

    [SerializeField]
    AudioClip pole1;

    [SerializeField]
    AudioClip pole2;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    public void Table1()
    {
        aud.clip = table1;
        StartAudio();
    }
    public void Table2()
    {
        aud.clip = table2;
        StartAudio();
    }
    public void Table3()
    {
        aud.clip = table3;
        StartAudio();
    }
    public void Table4()
    {
        aud.clip = table4;
        StartAudio();
    }
    public void Table5()
    {
        aud.clip = table5;
        StartAudio();
    }
    public void Pole1()
    {
        aud.clip = pole1;
        StartAudio();
    }
    public void Pole2()
    {
        aud.clip = pole2;
        StartAudio();
    }

    void StartAudio()
    {
        aud.Play();
    }
}
