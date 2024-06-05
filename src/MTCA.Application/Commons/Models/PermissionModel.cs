using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class PermissionModel
{
    public string ResourceName { get; set; }
    public bool Create {  get; set; } = false;
    public bool Read {  get; set; } = false;
    public bool Update {  get; set; } = false;
    public bool Delete {  get; set; } = false;
}
