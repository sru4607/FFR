using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;

namespace ActualGame
{
    public class Soundtrack
    {
        // Fields
        private static Section currentSection;
        private Section lead;
        private List<Section> loop;
        private Section trail;

        public bool IsPlaying { get; private set; }

        // Constructor
        public Soundtrack(Song loopStart, int loopBpm, int loopMeasures, int loopBeatsPerMeasure)
        {
            loop = new List<Section>();
            Section loop1 = new Section(loopStart, loopBpm, loopMeasures, loopBeatsPerMeasure);
            loop1.Next = loop1;
            loop.Add(loop1);
        }

        public void SetLead(Song lead, int bpm, int measures, int beatsPerMeasure)
        {
            this.lead = new Section(lead, bpm, measures, beatsPerMeasure);
            this.lead.Next = loop[0];
        }

        public void AddToLoop(Song loopSection, int bpm, int measures, int beatsPerMeasure)
        {
            Section loopNext = new Section(loopSection, bpm, measures, beatsPerMeasure);
            loop[loop.Count - 1].Next = loopNext;
            loopNext.Next = loop[0];
            loop.Add(loopNext);
        }

        public void SetTrail(Song trail, int bpm, int measures, int beatsPerMeasure)
        {
            this.trail = new Section(trail, bpm, measures, beatsPerMeasure);
        }

        public void Start()
        {
            MediaPlayer.Play(lead.Song);
            currentSection = lead;
            IsPlaying = false;
        }

        public void Update()
        {
            if (MediaPlayer.State == MediaState.Stopped || !(loop.Contains(currentSection) || (lead != null && currentSection == lead) || (trail!=null && currentSection == trail)))
            {
                if (lead != null)
                {
                    currentSection = lead;
                }
                else
                {
                    currentSection = loop[0];
                }

                MediaPlayer.Play(currentSection.Song);
            }
            else
            {
                //MediaPlayer.Pause();
                if (MediaPlayer.PlayPosition >= currentSection.Length || MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Stop();
                    currentSection = currentSection.Next;
                    MediaPlayer.Play(currentSection.Song, TimeSpan.Zero);
                }
                //MediaPlayer.Resume();
            }
        }

        public static void Stop()
        {
            currentSection = null;
            MediaPlayer.Stop();
            MediaPlayer.Queue.Equals(null);
        }

        // Private Section class used to track the flow of the song
        private class Section
        {
            // Fields
            public Song Song { get; }
            public Section Next { get; set; }
            public TimeSpan Length { get; }

            // Constructor
            public Section(Song song, int bpm, int measures, int beatsPerMeasure)
            {
                Song = song;

                int length = measures * beatsPerMeasure * 60 *1000 / bpm;

                int milliseconds = length % 1000 - 50;
                length /= 1000;
                int seconds = length % 60;
                length /= 60;
                int minutes = length % 60;
                length /= 60;
                int hours = length % 24;
                length /= 24;

                Length = new TimeSpan(length, hours, minutes, seconds, milliseconds);
            }
        }
    }
}
