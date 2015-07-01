using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Core.Interface;
using QsTech.Framework.Data;
using QsTech.Framework.Security;

namespace QsTech.Core.Models.ActionStatuss
{
    public class ActionStatusManager : IActionStatusManager
    {
        private readonly IRepository<ActionStatus> _repActionStatus;
        private readonly IAuthenticationService _authenticationService;
        private readonly IOwnerEnterpriseManager _ownerEnterpriseManager;
        public ActionStatusManager(IRepository<ActionStatus> repActionStatus,
            IAuthenticationService authenticationService,
            IOwnerEnterpriseManager ownerEnterpriseManager)
        {
            _repActionStatus = repActionStatus;
            _authenticationService = authenticationService;
            _ownerEnterpriseManager = ownerEnterpriseManager;
        }

        public void AddActionStatus(int type, string entityId, string entityCode, string action, string actionDescription)
        {
            var user = _authenticationService.GetAuthenticatedUser();
            var enterprise = _ownerEnterpriseManager.GetOwner(user);
            var actions = new ActionStatus()
            {
                Type = type,
                OperatorId = user.Id,
                OperatorName = user.Name,
                OperatorDate = System.DateTime.Now,
                EntityId = entityId,
                EntityCode = entityCode,
                Action = action,
                ActionDescription = actionDescription,
                OwnerId=enterprise.Id,
                OwnerName=enterprise.Short==null?enterprise.NameCn:enterprise.Short
            };
            _repActionStatus.Create(actions);
        }
    }
}