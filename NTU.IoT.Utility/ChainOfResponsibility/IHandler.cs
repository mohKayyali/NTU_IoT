using System;
namespace NTU.IoT.Utility.ChainOfResponsibility
{
	public interface IHandler
	{
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }
}

