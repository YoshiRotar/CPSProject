using CPSProject.Data.Signal;
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
                Signal = new UnitaryNoise(frequency, amplitude, startingMoment, duration);
            }
            if (id == 1)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                Signal = new GaussianNoise(frequency, amplitude, startingMoment, duration);
            }
            if (id == 2)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                Signal = new SinusoidalSignal(frequency, amplitude, period, startingMoment, duration);
            }
            if (id == 3)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                Signal = new SinusoidalSignalHalfRectified(frequency, amplitude, period, startingMoment, duration);
            }
            if (id == 4)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                Signal = new SinusoidalSignalFullRectified(frequency, amplitude, period, startingMoment, duration);
            }
            if (id == 5)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
                Signal = new RectangularSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
            }
            if (id == 6)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
                Signal = new RectangularSimetricalSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
            }
            if (id == 7)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.PeriodText, out period)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.DutyCycleText, out dutyCycle)) return false;
                Signal = new TriangularSignal(frequency, amplitude, period, startingMoment, duration, dutyCycle);
            }
            if (id == 8)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.TimeOfStepText, out timeOfStep)) return false;
                Signal = new StepFunctionSignal(frequency, amplitude, startingMoment, duration, timeOfStep);
            }
            if (id == 9)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!int.TryParse(properties.NumberOfAllSamplesText, out numberOfAllSamples)) return false;
                if (!int.TryParse(properties.NumberOfSampleText, out numberOfSample)) return false;
                Signal = new KroneckerDelta(frequency, amplitude, startingMoment, numberOfAllSamples, numberOfSample);
            }
            if (id == 10)
            {
                if (!double.TryParse(properties.FrequencyText, out frequency)) return false;
                if (!double.TryParse(properties.AmplitudeText, out amplitude)) return false;
                if (!double.TryParse(properties.StartingMomentText, out startingMoment)) return false;
                if (!double.TryParse(properties.DurationText, out duration)) return false;
                if (!double.TryParse(properties.ProbabilityText, out probability)) return false;
                Signal = new ImpulseNoise(frequency, amplitude, startingMoment, duration, probability);
            }
            return true;
        }
    }
}
