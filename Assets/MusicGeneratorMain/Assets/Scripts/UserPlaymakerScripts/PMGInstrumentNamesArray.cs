#if PMG_PLAYMAKER
using UnityEngine;
using System.Collections;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Procedural Music Generator")]
    [Tooltip("Create an Array of all Instrument names in the current InstrumentSet.")]
    public class PMGInstrumentNamesArray : FsmStateAction
    {

	[RequiredField]
	[CheckForComponent(typeof(MusicGenerator))]
	[Tooltip("the target. A MusicGenerator component is required.")]
	public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
	[ArrayEditor(VariableType.String)]
        [Tooltip("The Array of Instrument names.")]
        public FsmArray names;


        public override void Reset()
        {
            names = null;
        }

        public override void OnEnter()
        {

		MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();


            if (mMusicGenerator != null)
            {
            int count = mMusicGenerator.InstrumentSet.Instruments.Count;
            names.Reset();
	names.Resize(count);

            for (int i = 0; i < count; i++)
                {
			var instrument = mMusicGenerator.InstrumentSet.Instruments[i];
			var myname = instrument.InstrumentData.InstrumentName;
                	names.Set(i, myname);
                }
            }
            Finish();
        }



    }
}
#endif
