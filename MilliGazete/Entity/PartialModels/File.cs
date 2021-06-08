using Entity.Abstract;

namespace Entity.Models
{
    public partial class File : IEntity
    {
        public string GetFullFilePath()
        {
            return IsCdnFile.Value ? FileName : "https://milligazete.istmedyaapi.com/" + FileName;
        }
    }
}
