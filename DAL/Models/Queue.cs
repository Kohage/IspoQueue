namespace IspoQueue.DAL.Models;

public class Queue {
    public Guid Id { get; set; }
    public string? Number { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
    public int? StatusId { get; set; }
    public Guid? WindowId { get; set; }

}