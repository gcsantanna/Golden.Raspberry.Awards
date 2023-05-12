public class ProducerIndication
{
    public string Producer { get; set; }
    public int Interval => FollowingWin - PreviousWin;
    public int PreviousWin { get; set; }
    public int FollowingWin { get; set; }
}

