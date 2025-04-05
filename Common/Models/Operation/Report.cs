

namespace ScalperPlus.Common.Models.Operation;

public class Report : Exception, IModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}
