using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProcGenMusic
{
public class NewAudioPlayer : MonoBehaviour {


    [SerializeField]
    private MusicGenerator mMusicGenerator;

    [SerializeField]
    private int[] mInstrumentIndices;

    private NewInstrumentHandler[] mInstrumentHandlers;

    void Start() {

        mInstrumentHandlers = new NewInstrumentHandler[mInstrumentIndices.Length];
        for (var index = 0; index < mInstrumentHandlers.Length; index++)
        {
            mInstrumentHandlers[index] = new NewInstrumentHandler();
            mInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[index]);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("wand")) 
        {

            // Play mInstrumentHandlers[0]
            mInstrumentHandlers[0].PlayNote();

            Debug.Log("Plant sound played.");
        }
    }
}
}