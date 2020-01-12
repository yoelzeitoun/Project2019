using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class Factory_BL
    {
        public static IBL getBL()
        {
            return BL.getMyBL();
        }
    }
}
