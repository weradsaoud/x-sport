using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.Common.Models
{
    public static class DayOfWeekTranslations
    {
        public static readonly Dictionary<DayOfWeek, string> DayOfWeekInArabic =
            new Dictionary<DayOfWeek, string>()
        {
            { DayOfWeek.Sunday, "الأحد" }, // al-ahad
            { DayOfWeek.Monday, "الإثنين" }, // al-ithnayn
            { DayOfWeek.Tuesday, "الثلاثاء" }, // ath-thulaathaa'
            { DayOfWeek.Wednesday, "الأربعاء" }, // al-arbi'aa'
            { DayOfWeek.Thursday, "الخميس" }, // al-khamīs
            { DayOfWeek.Friday, "الجمعة" }, // al-jumuʿah
            { DayOfWeek.Saturday, "السبت" }, // as-sabt
        };
    }
}
