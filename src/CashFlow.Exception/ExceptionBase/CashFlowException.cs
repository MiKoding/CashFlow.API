using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Exception.ExceptionBase;
public abstract class CashFlowException : SystemException
{
    protected CashFlowException(string message) : base(message) 
    {
        
    }
}
