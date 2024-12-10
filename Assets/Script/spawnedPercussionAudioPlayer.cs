using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProcGenMusic
{
    public class spawnedPercussionAudioPlayer : MonoBehaviour {

        [SerializeField]
        private MusicGenerator mMusicGenerator;

        [SerializeField]
        private int[] mInstrumentIndices;

        private NewInstrumentHandler[] newInstrumentHandlers;

        void Awake() {

            newInstrumentHandlers = new NewInstrumentHandler[mInstrumentIndices.Length];
            for (var index = 0; index < newInstrumentHandlers.Length; index++)
            {
                newInstrumentHandlers[index] = new NewInstrumentHandler();
                // newInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[index]);
                int randomIndex = GetRandomNumber();
                newInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[randomIndex]);
            }
        }

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("wand")) 
            {
                newInstrumentHandlers[0].PlayNote();
            }
        }

        // This function returns a random number from either 0-69 or 170-212, with equal likelihood across both ranges
        public int GetRandomNumber()
        {
            // Calculate the total number of possible values across both ranges
            int totalValues = 70 + 43;  // 70 values in the first range, 43 values in the second range
            
            // Pick a random index between 0 and totalValues - 1
            return Random.Range(70, 170);
        }
    }
}