using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class ExecutorItem
{
    public ProductInfo ProductInfo { get; set; }
    public List<Guid> Executors { get; set; } = new List<Guid>();
}

public class ExecutorDocumentDetail
{
    public Guid DocumentId { get; set; }
    public Guid? DocumentDetailId {  get; set; }
    public List<Guid> Executors { get; set; } = new List<Guid>();
}