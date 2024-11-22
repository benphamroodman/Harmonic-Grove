#if PMG_PLAYMAKER
using UnityEngine;
using System.Collections;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Procedural Music Generator")]
    [Tooltip("GET an Array of the current Harmonic Chord Progression. To SET the chords use Leitmotif Chords Array.")]
    public class PMGProgressionChordsArray : FsmStateAction
    {

	[RequiredField]
	[CheckForComponent(typeof(MusicGenerator))]
	[Tooltip("the target. A MusicGenerator component is required.")]
	public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
	[ArrayEditor(VariableType.Int)]
        [Tooltip("The Array of the Chord Progression.")]
        public FsmArray chordProgression;




        public override void Reset()
        {
            chordProgression = null;
        }


        public override void OnEnter()
        {

		MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();


            if (mMusicGenerator != null)
            {
		int count = mMusicGenerator.CurrentChordProgression.Count;
		chordProgression.Reset();
		chordProgression.Resize(count);

		for (int i = 0; i < count; i++)
                {
			var mychord = mMusicGenerator.CurrentChordProgression[i];
                	chordProgression.Set(i, mychord);
                }
            }

			Finish();

        }

 





    }
}
#endif
