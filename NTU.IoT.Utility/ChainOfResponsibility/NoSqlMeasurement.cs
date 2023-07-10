using System;
using NTU.IoT.Models;

namespace NTU.IoT.Utility.ChainOfResponsibility
{
	public class NoSqlMeasurementCreation: AbstractHandler
	{
        public override object Handle(object request)
        {
            System.Diagnostics.Debug.WriteLine("NoSqlMeasurement " + ((DeviceType)request).name);

            return base.Handle(request);
        }
    }
}

