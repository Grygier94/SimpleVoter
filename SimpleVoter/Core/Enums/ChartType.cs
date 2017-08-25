using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleVoter.Core.Enums
{
    public enum ChartType
    {
        Bar = 1,
        Doughnut = 2,
        Pie = 3,
        PolarArea = 4,
        Radar = 5
    }
}