using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    FMOD.Studio.EventInstance instance;

    void Start()
    {

    }

    public void playFragment(int ID)
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Frag" + ID + "_yoporti");
        instance.start();
    }
}
