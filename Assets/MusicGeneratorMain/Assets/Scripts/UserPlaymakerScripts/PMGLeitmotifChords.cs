#if PMG_PLAYMAKER
using UnityEngine;
using System.Collections;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Procedural Music Generator")]
    [Tooltip("SET the Leitmotif Chords individually. To GET the currently playing chords use Progression Chord Array.")]
    public class PMGLeitmotifChords : FsmStateAction
    {

	[RequiredField]
	[CheckForComponent(typeof(MusicGenerator))]
	[Tooltip("the target. A MusicGenerator component is required.")]
	public FsmOwnerDefault gameObject;


	[ActionSection("Leitmotif Chord Progression (values are 1 - 7).")]  

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord1;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord2;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord3;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord4;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord5;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord6;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord7;

	[Tooltip("the chord as int (values are 1 - 7).")]
	[UIHint(UIHint.Variable)]
       public FsmInt leitmotifChord8;


	public bool everyFrame;


        public override void Reset()
        {
		leitmotifChord1 = null;
		leitmotifChord2 = null;
		leitmotifChord3 = null;
		leitmotifChord4 = null;
		leitmotifChord5 = null;
		leitmotifChord6 = null;
		leitmotifChord7 = null;
		leitmotifChord8 = null;
        }


        public override void OnEnter()
        {

		MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();


            if (mMusicGenerator != null)
            {


			if (leitmotifChord1.Value > 0 && leitmotifChord1.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[0] = leitmotifChord1.Value;
			}

			if (leitmotifChord2.Value > 0 && leitmotifChord2.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[1] = leitmotifChord2.Value;
			}

			if (leitmotifChord3.Value > 0 && leitmotifChord3.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[2] = leitmotifChord3.Value;
			}

			if (leitmotifChord4.Value > 0 && leitmotifChord4.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[3] = leitmotifChord4.Value;
			}

			if (leitmotifChord5.Value > 0 && leitmotifChord5.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[4] = leitmotifChord5.Value;
			}

			if (leitmotifChord6.Value > 0 && leitmotifChord6.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[5] = leitmotifChord6.Value;
			}

			if (leitmotifChord7.Value > 0 && leitmotifChord7.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[6] = leitmotifChord7.Value;
			}

			if (leitmotifChord8.Value > 0 && leitmotifChord8.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[7] = leitmotifChord8.Value;
			}



		}

 		if (!everyFrame)
		{
			Finish();
		}

	}


	        public override void OnUpdate()
	        {
		MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();


            if (mMusicGenerator != null)
            {



			if (leitmotifChord1.Value > 0 && leitmotifChord1.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[0] = leitmotifChord1.Value;
			}

			if (leitmotifChord2.Value > 0 && leitmotifChord2.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[1] = leitmotifChord2.Value;
			}

			if (leitmotifChord3.Value > 0 && leitmotifChord3.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[2] = leitmotifChord3.Value;
			}

			if (leitmotifChord4.Value > 0 && leitmotifChord4.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[3] = leitmotifChord4.Value;
			}

			if (leitmotifChord5.Value > 0 && leitmotifChord5.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[4] = leitmotifChord5.Value;
			}

			if (leitmotifChord6.Value > 0 && leitmotifChord6.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[5] = leitmotifChord6.Value;
			}

			if (leitmotifChord7.Value > 0 && leitmotifChord7.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[6] = leitmotifChord7.Value;
			}

			if (leitmotifChord8.Value > 0 && leitmotifChord8.Value < 8)
			{
			mMusicGenerator.ConfigurationData.LeitmotifProgression[7] = leitmotifChord8.Value;
			}



		}

	 }





    }
}
#endif
