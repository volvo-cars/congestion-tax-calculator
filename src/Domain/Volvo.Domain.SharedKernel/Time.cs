using System;
using System.Runtime.Serialization;

namespace Volvo.Domain.SharedKernel
{
    [Serializable]
    public struct Time : IComparable, ISerializable
    {
        public Time(int hours, int minutes, int seconds)
        {
            if (hours < 0 || hours > 23) 
                throw new ArgumentOutOfRangeException(nameof(hours));

            if (minutes < 0 || minutes > 60)
                throw new ArgumentOutOfRangeException(nameof(minutes));

            if (seconds < 0 || seconds > 60)
                throw new ArgumentOutOfRangeException(nameof(minutes));

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time(int hours, int minutes) : this(hours, minutes, 0)
        {
        }

        public Time(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            Hours = info.GetInt32(nameof(Hours));
            Minutes = info.GetInt32(nameof(Minutes));
            Seconds = info.GetInt32(nameof(Seconds));
        }

        public int Hours { get; }

        public int Minutes { get; }

        public int Seconds { get; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is not Time otherTime) throw new ArgumentException($"Object is not {nameof(Time)}");

            var hoursComparisation = Hours.CompareTo(otherTime.Hours);

            switch (hoursComparisation)
            {
                case < 0:
                    return hoursComparisation;
                case > 0:
                    return hoursComparisation;
                default:
                {
                    var minutesComparisation = Minutes.CompareTo(otherTime.Minutes);

                    switch (minutesComparisation)
                    {
                        case < 0:
                            return minutesComparisation;
                        case > 0:
                            return minutesComparisation;
                        default:
                        {
                            var secondsComparisation = Seconds.CompareTo(otherTime.Seconds);

                            return secondsComparisation switch
                            {
                                < 0 => secondsComparisation,
                                > 0 => secondsComparisation,
                                _ => 0
                            };
                        }
                    }
                }
            }
        }

        public static bool operator >=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) >= 0;
        }

        public static bool operator <=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) <= 0;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            info.AddValue(nameof(Hours), Hours);
            info.AddValue(nameof(Minutes), Minutes);
            info.AddValue(nameof(Seconds), Seconds);
        }
    }
}