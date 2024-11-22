using System.Collections.Generic;

#pragma warning disable 0649

namespace ProcGenMusic
{
    /* This class is a bit ugly. For percussion, each instrument will function as its own
     * DisplayEditor, so, this acts more as a parent forwarding each command to the children
     */
    public class LeitmotifPercussionEditor : PercussionEditorBase
    {
        ///<inheritdoc/>
        public override void Initialize(UIManager uiManager)
        {
            mDisplayEditor = uiManager.UILeitmotifEditor;
            base.Initialize(uiManager);
        }

        ///<inheritdoc/>
        protected override void UpdateClipNote(MeasureEditorNoteData noteData, bool wasAdded, Instrument instrument)
        {
            var note = new LeitmotifNote(0);
            var notes = instrument.InstrumentData.Leitmotif.NotesDictionaryReadonly();
            if (wasAdded)
            {
                instrument.InstrumentData.Leitmotif.AddLeitmotifNote(noteData.Measure, noteData.Beat.x, noteData.Beat.y, note);
            }
            else
            {
                instrument.InstrumentData.Leitmotif.RemoveLeitmotifNote(noteData.Measure, noteData.Beat.x, noteData.Beat.y, note);
            }

            if (instrument.InstrumentData.Leitmotif.TryGetLeitmotifNotes(noteData.Measure, noteData.Beat.x, noteData.Beat.y, out var leitmotifNotes))
            {
                var index = Leitmotif.GetUnscaledNoteArray(leitmotifNotes, mUIManager.MusicGenerator);
                foreach (var timestepNotes in index)
                {
                    mUIManager.MusicGenerator.PlayNote(mUIManager.CurrentInstrumentSet,
                        instrument.InstrumentData.Volume,
                        instrument.InstrumentData.InstrumentType,
                        timestepNotes,
                        instrument.InstrumentIndex);
                }
            }
        }
    }
}
