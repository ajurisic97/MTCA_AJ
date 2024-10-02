using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Domain.Repositories;
public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection(string? conn = null);
}
