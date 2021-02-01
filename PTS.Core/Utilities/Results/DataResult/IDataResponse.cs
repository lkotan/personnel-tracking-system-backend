using PTS.Core.Utilities.Results.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Core.Utilities.Results.DataResult
{
    public interface IDataResponse<out T>:IResponse
    {
        T Data { get; }
    }
}
