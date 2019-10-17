using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Weather
{
    public class WeatherData
    {
        public static IEnumerable<WeatherObservation> ReadRange(
            TextReader text,
            DateTime? start = null,
            DateTime? end = null,
            Action<string> errorHandler = null)
        {
            return ReadAll(text, errorHandler).SkipWhile(a => a.TimeStamp <
            (start ?? DateTime.MinValue))
                .TakeWhile(a => a.TimeStamp <= (end ?? DateTime.MaxValue));
        }


        public static IEnumerable<WeatherObservation> ReadAllWithoutYeild(TextReader text, Action<string> errorHandler = null)
        {
            string line = null;
            var list = new List<WeatherObservation>();
            while ((line = text.ReadLine()) != null)
            {
                if (WeatherObservation.TryParse(line, out WeatherObservation wo))
                {
                    list.Add(wo);
                }
                else
                {
                    try
                    {
                        errorHandler?.Invoke(line);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            return list;
        }


        public static IEnumerable<WeatherObservation> ReadAll(TextReader text, Action<string> errorHandler = null)
        {
            string line = null;
            while((line=text.ReadLine()) != null)
            {
                if(WeatherObservation.TryParse(line,out WeatherObservation wo))
                {
                    yield return wo;
                }
                else
                {
                    try
                    {
                        errorHandler?.Invoke(line);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
