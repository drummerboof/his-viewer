using System;
using System.Collections.Generic;

namespace HisViewer
{
    class ImageAdjustments
    {
        public static string CONTRAST = "CONTRAST";

        private Dictionary<String, ImageAdjustmentInterface> AdjustmentsMap = new Dictionary<string, ImageAdjustmentInterface>();

        public void Register (string name, ImageAdjustmentInterface adjustment)
        {
            AdjustmentsMap.Add(name, adjustment);
        }

        public ImageAdjustmentInterface Get (string name)
        {
            return AdjustmentsMap[name];
        }
    }
}
