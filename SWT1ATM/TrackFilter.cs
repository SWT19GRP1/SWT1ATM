using System;
using SWT1ATM.Interfaces;
using TransponderReceiver;

namespace SWT1ATM
{
    #region DTO
    public class TrackfilterDTO
    {
        public string Tag { get; }
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public DateTime Time { get; }
        


        public TrackfilterDTO(string tag, int x, int y, int z, DateTime time)
        {
            Tag = tag;
            X = x;
            Y = y;
            Z = z;
            Time = time;
        }
    }
    #endregion
    

    public class TrackFilter : ITrackFilter
    {
       public event EventHandler<FormattedTransponderDataEventArgs> FormattedDataEvent;

        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int ZOffset { get; set; }
        public int XLength { get; set; }
        public int YWidth { get; set; }
        public int ZHeight { get; set; }
        public TrackFilter(ITransponderReceiver reciever, int xOffset = 0, int yOffset = 0,
            int zOffset = 500, int xLength = 80000, int yWidth = 80000, int zHeight = 19500)
        {
            reciever.TransponderDataReady += HandlerOnRaiseTrackInsideMonitoringAreaEvent;

            XOffset = xOffset;
            YOffset = yOffset;
            ZOffset = zOffset;
            XLength = xLength;
            YWidth = yWidth;
            ZHeight = zHeight;
        }

        public void HandlerOnRaiseTrackInsideMonitoringAreaEvent(object sender, RawTransponderDataEventArgs e)
        {
            char[] separators = { ';' };

            foreach (var data in e.TransponderData)
            {

                var tokens = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string tag = tokens[0];
                var xCoordinate = int.Parse(tokens[1]);
                var yCoordinate = int.Parse(tokens[2]);
                var zCoordinate = int.Parse(tokens[3]);
                var dateTime = GetDate(tokens[4]);

                if (xCoordinate <= (XOffset + XLength))
                {
                    if (yCoordinate <= YOffset + YWidth)
                    {
                        if (zCoordinate <= (zCoordinate + ZHeight))
                        {
                            TrackfilterDTO DTO = new TrackfilterDTO(tag,xCoordinate,yCoordinate,zCoordinate,dateTime);
                            OnFormattedDataEvent(DTO);
                        }
                    }
                }
            }
        }

        public virtual void OnFormattedDataEvent(TrackfilterDTO DTO)
        {
            FormattedDataEvent?.Invoke(this, new FormattedTransponderDataEventArgs(DTO));
        }

        public DateTime GetDate(string date)
        {
            var year   = int.Parse(date.Substring(0,4));
            var month  = int.Parse(date.Substring(4,2));
            var day    = int.Parse(date.Substring(6, 2));
            var hour   = int.Parse(date.Substring(8, 2));
            var minute = int.Parse(date.Substring(10, 2));
            var second = int.Parse(date.Substring(12, 2));
            var milli  = int.Parse(date.Substring(14, 3));
            var returnDateTime= new DateTime(year,month,day,hour,minute,second,milli);

            return returnDateTime;
        }
    }
}
