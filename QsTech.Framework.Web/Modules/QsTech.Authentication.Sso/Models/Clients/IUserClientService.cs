using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QsTech.Framework;
using QsTech.Framework.Pagination;
using QsTech.Framework.Data;
using QsTech.Framework.Utility;

namespace QsTech.Authentication.Sso.Clients
{
    public interface IUserClientService : ITransientDependency
    {
        IEnumerable<UserClient> GetAllUsers();

        UserClient GetUserClient(int id);

        IPagedList<UserClient> GetUsersByClientId(SearchParameterBase model);

        IPagedList<UserClient> GetUsersByClientId(IQueryParamter query,int pageIndx,int pageCount);

        void AddUserClients(List<UserClient> userClients);

        void DeleteUserClient(UserClient userClient);

        IEnumerable<UserClient> GetUserClientByUserId(int userId);

        IEnumerable<UserClient> GetUserClientByClientId(int clientId);
    }

    public class UserClientSearchModel : SearchParameterBase
    {
        public int? clientId { get; set; }
    }

    public class UserClientService : IUserClientService
    {
        private readonly IRepository<UserClient> _repUserClient;

        public UserClientService(IRepository<UserClient> repUserClient)
        {
            _repUserClient = repUserClient;
        }


        public IEnumerable<UserClient> GetAllUsers()
        {
            return _repUserClient.Table;
        }

        public IPagedList<UserClient> GetUserClients(string keyword, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _repUserClient.Table.OrderBy(m=>m.Id).ToPagedList(pageIndex, pageSize);
            }
            return _repUserClient.Table.ToPagedList(pageIndex, pageSize);
        }

        public UserClient GetUserClient(int id)
        {
            return _repUserClient.Fetch(m => m.Id == id).SingleOrDefault();
        }

        public IPagedList<UserClient> GetUsersByClientId(SearchParameterBase model)
        {
            var search = (UserClientSearchModel)model;
            var data = _repUserClient.Fetch(m => m.ClientId == search.clientId);
            if (!string.IsNullOrEmpty(search.keyword))
            {
                data.Where(m => m.UserName.Contains(search.keyword));
            }
            return data.OrderBy(m => m.Id).ToPagedList(search.page,search.limit);
        }

        public IEnumerable<UserClient> GetUserClientByClientId(int clientId)
        {
            return _repUserClient.Fetch(m => m.ClientId == clientId);
        }


        public IPagedList<UserClient> GetUsersByClientId(IQueryParamter model, int pageIndx, int pageCount)
        {
            var search = (UserClientQueryModel)model;
            var data = _repUserClient.Fetch(m => m.ClientId == search.ClientId);
            if (!string.IsNullOrEmpty(search.keyword))
            {
                data.Where(m => m.UserName.Contains(search.keyword));
            }
            return data.OrderBy(m => m.Id).ToPagedList(pageIndx, pageCount);
        }

        public void AddUserClients(List<UserClient> userClients)
        {
            foreach (var item in userClients)
            {
                _repUserClient.Create(item);
            }
        }

        public void DeleteUserClient(UserClient userClient)
        {
            _repUserClient.Delete(userClient);
        }

        public IEnumerable<UserClient> GetUserClientByUserId(int userId)
        {
            return _repUserClient.Fetch(m => m.UserId==userId);
        }
    }

}