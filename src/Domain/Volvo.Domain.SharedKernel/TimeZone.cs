using System;
using System.Runtime.Serialization;

namespace Volvo.Domain.SharedKernel
{
    [Serializable]
    public class TimeZone : ISerializable
    {
        private readonly Time _from;
        private readonly Time _to;

        public TimeZone(Time @from, Time to)
        {
            _from = @from;
            _to = to;
        }

        protected TimeZone(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            _from = (Time)info.GetValue(nameof(From), typeof(Time));
            _to = (Time)info.GetValue(nameof(To), typeof(Time));
        }

        public Time From => _from;
        public Time To => _to;

        public bool IsInTimeZone(DateTime dateTime) =>
            IsInTimeZone(new Time(dateTime.Hour, dateTime.Minute, dateTime.Second));

        public bool IsInTimeZone(Time time) => time >= _from && time <= _to;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(From), _from, typeof(Time));
            info.AddValue(nameof(To), _to, typeof(Time));
        }
    }
}