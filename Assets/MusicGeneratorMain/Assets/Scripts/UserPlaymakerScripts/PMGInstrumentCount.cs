#if PMG_PLAYMAKER
using UnityEngine;
using ProcGenMusic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Procedural Music Generator")]
    [Tooltip("Gets the number of Instruments in the current config.")]
    public class PMGInstrumentCount : FsmStateAction
    {

	[RequiredField]
	[CheckForComponent(typeof(MusicGenerator))]
	[Tooltip("the target. A MusicGenerator component is required.")]
	public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)] 
        [Tooltip("The number of Instruments.")] 
        public FsmInt count;


        public override void Reset()
        {
            count = null;
        }

// Code that runs on entering the state.

	public override void OnEnter()
	{
		MusicGenerator mMusicGenerator = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<MusicGenerator>();
		if (mMusicGenerator != null)
		{
			count.Value = mMusicGenerator.InstrumentSet.Instruments.Count;
			Finish();
		}
		Finish();
	}
}

}
#endif
