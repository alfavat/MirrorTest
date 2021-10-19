using Entity.Enums;
using System.Collections.Generic;

namespace Business.Managers.Abstract
{
    public interface IBaseService
    {
        int[] DefaultUserOperationClaims { get; }
        bool IsEmployee
        { get; }
        Languages UserLanguage
        { get; }
        int RequestUserId
        { get; }
        bool IsCurrentUserPassive
        { get; }
        string UserIpAddress { get; }
        void UpdatePassiveUserList(int userId, bool active);
        void FillPassiveUserList(List<int> users);
        List<int> GetPassiveUserList { get; }
    }
}
