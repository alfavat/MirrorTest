using Business.Helpers.Abstract;

namespace Business.Helpers.Concrete
{
    public class CategoryHelper : ICategoryHelper
    {
        public string CreateCategoryCode(string maxCategoryCode)
        {
            string mainCategoryCode = maxCategoryCode.Substring(0, maxCategoryCode.Length - 3);
            string subCategoryCode = maxCategoryCode.Substring(maxCategoryCode.Length - 3, 3);
            string newSubCategoryCode = (subCategoryCode.ToInt32() + 1).ToString().PadLeft(3, '0');
            return mainCategoryCode + newSubCategoryCode;
        }
    }
}
