using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proteinDetect
{
    [Serializable]
    public class ConcentrationInfo
    {
        private float aveNSE = 0.0f;
        private float aveCEA = 0.0f;
        private float aveCY211 = 0.0f;
        private float aveDKK1 = 0.0f;

        public ConcentrationInfo()
        {
            
        }

        public ConcentrationInfo(float nse, float cea, float cy211, float dkk1)
        {
            this.aveCEA = cea;
            this.aveCY211 = cy211;
            this.aveDKK1 = dkk1;
            this.aveNSE = nse;
        }

        public float AveNSE 
        {
            get { return this.aveNSE; }
            set { this.aveNSE = value; }
        }

        public float AveCEA
        {
            get { return this.aveCEA; }
            set { this.aveCEA = value; }
        }

        public float AveCY211
        {
            get { return this.aveCY211; }
            set { this.aveCY211 = value; }
        }

        public float AveDKK1
        {
            get { return this.aveDKK1; }
            set { this.aveDKK1 = value; }
        }
    }
}
