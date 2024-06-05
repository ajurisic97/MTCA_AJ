﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class WarehouseTransferDocumentItem
{
    public Guid ProductId { get; set; }
    public Guid MeasurementUnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal InputPrice { get; set; }
    public decimal OutputPrice { get; set; }
}
