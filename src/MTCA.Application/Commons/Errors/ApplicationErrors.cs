using MTCA.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Errors;
internal class ApplicationErrors
{

    public static class CommonError
    {
        public static readonly Error NoData = new Error(
            "NoData",
            "There are no data to fetch for specified entity!");
    }

}
