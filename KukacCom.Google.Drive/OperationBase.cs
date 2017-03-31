using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public abstract class OperationBase
    {
        protected OperationBase( Drive drive )
        {
            Drive = drive;
        }

        public Drive Drive { get; private set; }
    }
}
