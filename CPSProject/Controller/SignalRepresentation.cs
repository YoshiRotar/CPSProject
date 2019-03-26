using CPSProject.Data.Signal;
using CPSProject.Data.Signal.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Controller
{
    public class SignalRepresentation
    {
        public double frequency;
        public double amplitude;
        public double period;
        public double startingMoment;
        public double duration;
        public double dutyCycle;
        public double timeOfStep;
        public double probability;
        public int numberOfAllSamples;
        public int numberOfSample;

        internal ISignal Signal { get; set; }

        internal bool CanDraw(int id, SignalTextProperties properties)
        {
            if (id == -1) return false;
            if (id == 0)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
            }
            if (id == 1)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
            }
            if (id == 2)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
            }
            if (id == 3)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
            }
            if (id == 4)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
            }
            if (id == 5)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
            }
            if (id == 6)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
            }
            if (id == 7)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
            }
            if (id == 8)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.TimeOfStepText, out timeOfStep)) return false;
            }
            if (id == 9)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!int.TryParse(properties.NumberOfAllSamplesText, out numberOfAllSamples)) return false;
                if (!int.TryParse(properties.NumberOfSampleText, out numberOfSample)) return false;
            }
            if (id == 10)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.ProbabilityText, out probability)) return false;
            }
            if (frequency == 0) return false;
            return true;
        }
    }
}
