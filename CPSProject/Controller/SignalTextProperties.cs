using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSProject.Controller
{
    public class SignalTextProperties
    {
        public string FrequencyText { get; set; }
        public string AmplitudeText { get; set; } 
        public string PeriodText { get; set; }
        public string StartingMomentText { get; set; }
        public string DurationText { get; set; }
        public string DutyCycleText { get; set; }
        public string TimeOfStepText { get; set; }
        public string ProbabilityText { get; set; } 
        public string NumberOfAllSamplesText { get; set; }
        public string NumberOfSampleText { get; set; } 
        public string AverageValueText { get; set; } = "0";
        public string AbsouluteAverageValueText { get; set; } = "0";
        public string AveragePowerText { get; set; } = "0";
        public string VarianceText { get; set; } = "0";
        public string EffectiveValueText { get; set; } = "0";
    }
}
