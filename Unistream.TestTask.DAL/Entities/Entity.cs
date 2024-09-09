using System.ComponentModel.DataAnnotations;

namespace Unistream.TestTask.DAL.Entities;

public class Entity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime OperationDate { get; set; }
    public decimal Amount { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
        OperationDate = DateTime.Now;
    }
}
