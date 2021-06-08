using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int? UserId { get; set; }
        public string DateTime { get; set; }
        public List<LogParameter> LogParameters { get; set; }
        
    }
}
