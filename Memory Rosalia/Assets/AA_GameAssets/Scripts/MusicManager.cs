using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;

    [SerializeField] string song;

    void Start()
    {

    }

    public void playFragment(int ID)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Frag" + ID + song);
        instance.start();
    }
}
