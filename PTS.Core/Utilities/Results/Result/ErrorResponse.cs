using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Core.Utilities.Results.Result
{
    public class ErrorResponse:Response
    {
        public ErrorResponse(string message):base(false,message)
        {

        }
        public ErrorResponse():base(false)
        {

        }
    }
}
