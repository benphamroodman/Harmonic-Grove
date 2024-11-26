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

        private NewInstrumentHandler[] newInstrumentHandlers;

        void Start() {

            Debug.Log("Starting audio player!");
            newInstrumentHandlers = new NewInstrumentHandler[mInstrumentIndices.Length];
            for (var index = 0; index < newInstrumentHandlers.Length; index++)
            {
                newInstrumentHandlers[index] = new NewInstrumentHandler();
                newInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[index]);
            }
        }

        /**void Update() {
            // Play newInstrumentHandlers[0]
            newInstrumentHandlers[0].PlayNote();
        }**/

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("wand")) 
            {

                // Play newInstrumentHandlers[0]
                newInstrumentHandlers[0].PlayNote();

                Debug.Log("Plant sound played.");
            }
        }
    }
}