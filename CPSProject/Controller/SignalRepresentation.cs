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
        private double frequency;
        private double amplitude;
        private double period;
        private double startingMoment;
        private double duration;
        private double dutyCycle;
        private double timeOfStep;
        private double probability;
        private int numberOfAllSamples;
        private int numberOfSample;

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
                if (frequency != 0) Signal = new UnitaryNoise(frequency, amplitude, startingMoment, duration);
                else return false;
            }
            if (id == 1)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (frequency != 0) Signal = new GaussianNoise(frequency, amplitude, startingMoment, duration);
                else return false;
            }
            if (id == 2)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (frequency != 0) Signal = new SinusoidalSignal(frequency, amplitude, period, startingMoment, duration);
                else return false;
            }
            if (id == 3)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (frequency != 0) Signal = new SinusoidalSignalHalfRectified(frequency, amplitude, period, startingMoment, duration);
                else return false;
            }
            if (id == 4)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (frequency != 0) Signal = new SinusoidalSignalFullRectified(frequency, amplitude, period, startingMoment, duration);
                else return false;
            }
            if (id == 5)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
                if (frequency != 0) Signal = new RectangularSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
                else return false;
            }
            if (id == 6)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
                if (frequency != 0) Signal = new RectangularSimetricalSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
                else return false;
            }
            if (id == 7)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
                if (frequency != 0) Signal = new TriangularSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
                else return false;
            }
            if (id == 8)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.TimeOfStepText, out timeOfStep)) return false;
                if (frequency != 0) Signal = new StepFunctionSignal(frequency, amplitude, startingMoment, duration, timeOfStep);
                else return false;
            }
            if (id == 9)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!int.TryParse(properties.NumberOfAllSamplesText, out numberOfAllSamples)) return false;
                if (!int.TryParse(properties.NumberOfSampleText, out numberOfSample)) return false;
                if (frequency != 0) Signal = new KroneckerDelta(frequency, amplitude, startingMoment, numberOfAllSamples, numberOfSample);
                else return false;
            }
            if (id == 10)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.ProbabilityText, out probability)) return false;
                if (frequency != 0) Signal = new ImpulseNoise(frequency, amplitude, startingMoment, duration, probability);
                else return false;
            }
            return true;
        }
    }
}
