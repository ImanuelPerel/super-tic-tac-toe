using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Tic_Tac_Toe.Model;

public class AlreadyHasAValueException:Exception
{
    public AlreadyHasAValueException():base() { }
    public AlreadyHasAValueException(string msg):base(msg) {}
    public AlreadyHasAValueException (string msg, Exception inner) : base(msg, inner) { }
}
