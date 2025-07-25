public class FeatureCollection
{
    public Feature[] features { get; set; }
}

public class Feature
{
    public Properties properties { get; set; }
}

public class Properties
{
    public double mag { get; set; }
    public string place { get; set; }
    public long time { get; set; }
}