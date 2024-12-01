using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProcGenMusic
{
    public class spawnedMusicAudioPlayer : MonoBehaviour {

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
                int randomIndex = GetRandomNumber();
                newInstrumentHandlers[index].Initialize(mMusicGenerator, mInstrumentIndices[randomIndex]);
            }
        }

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("wand")) 
            {

                // Play newInstrumentHandlers[0]
                newInstrumentHandlers[0].PlayNote();

            }
        }

        // This function returns a random number from either 0-69 or 170-212, with equal likelihood across both ranges
        public int GetRandomNumber()
        {
            // Calculate the total number of possible values across both ranges
            int totalValues = 70 + 43;  // 70 values in the first range, 43 values in the second range
            
            // Pick a random index between 0 and totalValues - 1
            int randomIndex = Random.Range(0, totalValues);

            // If the random index falls within the first 70 values, return a number from 0 to 69
            if (randomIndex < 70)
            {
                return randomIndex;  // This will give a value between 0 and 69 (inclusive)
            }
            else
            {
                // If the index is in the second range (170-212), map it to that range
                return randomIndex + 170 - 70;  // This maps the index from 70-112 to 170-212
            }
        }
    }
}