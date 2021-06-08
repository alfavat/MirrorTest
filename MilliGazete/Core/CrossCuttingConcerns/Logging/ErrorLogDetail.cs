using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class ErrorLogDetail : LogDetail
    {
        public List<ErrorLogParameter> ErrorDetails { get; set; }
    }
}
