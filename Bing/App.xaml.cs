using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        public static string SecondToHMS(double sec)
        {


            double h = Math.Floor(sec / 3600);
            double m = sec - h * 3600;
            m = Math.Floor(m / 60);
            double s = sec - h * 3600 - m * 60;

            string strHour, strMinute, strSecond;



            if (h > 999)
            {
                strHour = h.ToString();
                strHour = strHour.Substring(0, 1) + "," + strHour.Substring(1);
            }
            else
            {
                if (h < 10)
                {
                    strHour = "0" + h.ToString();
                }
                else
                {
                    strHour = h.ToString();
                }

            }


            if (m < 10)
            {
                strMinute = "0" + m.ToString();
            }
            else
            {
                strMinute = m.ToString();
            }


            if (s < 10)
            {
                strSecond = "0" + s.ToString();
            }
            else
            {
                strSecond = s.ToString();
            }


            return strHour + ":" + strMinute + ":" + strSecond;
        }

    }
}
