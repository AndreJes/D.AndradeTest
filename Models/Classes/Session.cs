using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes
{
    public class Session
    {
        public ESessionType Type { get; private set; }
        private List<Lecture> Lectures {  get; set; } = [];
        public SessionEvent SessionEvent { get; private set; }

        public uint MaxLectureDuration { get; private set; }

        public uint CurrentDuration { get { return (uint)Lectures.Sum(lect => lect.Duration); } }

        public uint RemainingDuration { get { return MaxLectureDuration - CurrentDuration; } }

        public TimeSpan StartTime
        {
            get;
            private set;
        }

        public Session(ESessionType type)
        {
            Type = type;
            uint eventDuration;
            string eventDescription;
            TimeSpan eventMinStartTime;
            TimeSpan eventMaxStartTime;
            switch (Type)
            {
                case ESessionType.MATUTINAL:
                    eventDuration = 60;
                    eventDescription = "Almoço";
                    eventMinStartTime = new TimeSpan(12, 0, 0);
                    eventMaxStartTime = new TimeSpan(12, 0, 0);
                    StartTime = new TimeSpan(9, 0, 0);// 09:00H
                    MaxLectureDuration = 3 * 60; // 3 hours | 180 minutes
                    break;
                case ESessionType.VERPERTINE:
                    eventDuration = 60;
                    eventDescription = "Networking";
                    eventMinStartTime = new TimeSpan(16, 0, 0);
                    eventMaxStartTime = new TimeSpan(17, 0, 0);
                    StartTime = new TimeSpan(13, 0, 0);// 13:00H
                    MaxLectureDuration = 4 * 60; // 4 hours | 240 minutes
                    break;
                default:
                    throw new NotImplementedException($"Unsupported ESessionType: {Type}");
            }

            SessionEvent = new(eventDuration, eventDescription, eventMinStartTime, eventMaxStartTime);

            MaxLectureDuration += SessionEvent.EventAddedTime;
        }

        public TimeSpan GetLastLectureEndTime()
        {
            // if this is the first lecture being added, we'll use the session start time as default
            TimeSpan lectureStartTime = StartTime;

            // Otherwise we'll calculate the last added lecture startTime + Duration to get this new lecture start time
            if (Lectures.Count > 0)
            {
                var lastLecture = GetLastLecture();
                lectureStartTime = lastLecture.StartTime.Add(new TimeSpan(0, (int)lastLecture.Duration, 0));
            }

            return lectureStartTime;
        }

        public Lecture GetLastLecture()
        {
            return Lectures.Last();
        }

        public void AddLecture(Lecture lecture)
        {
            lecture.StartTime = GetLastLectureEndTime();
            Lectures.Add(lecture);
        }

        public override string ToString()
        {
            string sessionString = "";
            foreach (var lecture in Lectures)
            {
                sessionString += $"{lecture}\n";
            }
            sessionString += $"{SessionEvent}\n";
            return sessionString;
        }
    }
}
