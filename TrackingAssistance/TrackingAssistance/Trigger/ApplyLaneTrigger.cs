using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackingAssistance.Trigger
{
    public class ApplyLaneTrigger
    {
        private static ApplyLaneTrigger _instance;

        public static ApplyLaneTrigger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ApplyLaneTrigger();
            }
            return _instance;
        }

    }
}