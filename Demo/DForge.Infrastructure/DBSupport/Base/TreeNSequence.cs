namespace DForge.Infrastructure.DBSupport.Base;

public class DBTreeNSequence : DBTree, IDBTreeNSequence<string>
{
    [Column("Sequence")]
    [Sort("Sequence")]
    public string CurSequence { get; set; }

    [Column("FullSequence")]
    [Sort("FullSequence")]
    public string FullSequence
    {
        get { return _fullSequence; }
        set
        {
            string data = value;

            if (!data.StartsWith(","))
                data = "," + data;

            if (!data.EndsWith(","))
                data = data + ",";

            _fullSequence = data;
        }
    }

    private string _fullSequence;
}