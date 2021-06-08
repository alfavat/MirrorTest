﻿using System.Collections.Generic;

namespace Business.Managers.Abstract
{
    public interface IBaseService
    {
        bool IsEmployee
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
