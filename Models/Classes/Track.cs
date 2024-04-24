namespace Models.Classes
{
    public class Track()
    {
        public List<Session> Sessions { get; private set; } = [
                new Session(Enums.ESessionType.MATUTINAL), 
                new Session(Enums.ESessionType.VERPERTINE)
            ];

        public override string ToString()
        {

            string trackString = "";
            foreach (var session in Sessions)
            {
                trackString += session.ToString();
            }
            return trackString;
        }
    }
}
