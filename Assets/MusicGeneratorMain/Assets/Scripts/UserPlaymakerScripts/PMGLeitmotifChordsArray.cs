#if PMG_PLAYMAKER
using UnityEngine;
using System.Collections;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Procedural Music Generator")]
    [Tooltip("SET the Leitmotif Chord Progression with an Array. (1, 4, 4, 5, 1, 2, 2, 7). To GET the current playing chords use Progression Chords Array.")]
    public class PMGLeitmotifChordsArray : FsmStateAction
    {

	[RequiredField]
	[CheckForComponent(typeof(MusicGenerator))]
	[Tooltip("the target. A MusicGenerator component is required.")]
	public FsmOwnerDefault gameObject;

	[RequiredField]
	[Tooltip("Array to set the Leitmotif Chord Progression.")]
	[UIHint(UIHint.Variable)]
	[ArrayEditor(VariableType.Int)] 
	public FsmArray leitmotifChordArray;







        public override void Reset()
        {
		leitmotifChordArray = null;

        }


        public override void OnEnter()
        {

		MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();


            if (mMusicGenerator != null)
            {


		for (int i = 0; i < 8; i++)
			{

			int mychord = (int)leitmotifChordArray.Get(i);


			if (mychord > 0 && mychord < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[i] = mychord;
			}


			}
		}


			Finish();


	}







    }
}
#endif
