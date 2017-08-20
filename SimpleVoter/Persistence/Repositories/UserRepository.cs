using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Evernote.EDAM.Type;
using SimpleVoter.Core;
using SimpleVoter.Core.Enums;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly IApplicationDbContext Context;
        public UserRepository(IApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationUser Get(string userId)
        {
            return Context.Users.Single(u => u.Id == userId);
        }

        public IEnumerable<ApplicationUser> GetAll(PollTableInfo tableInfo)
        {
            var usersQuery = Context.Users
                .Where(u =>
                    u.Id.Contains(tableInfo.SearchText) ||
                    u.UserName.Contains(tableInfo.SearchText) ||
                    u.Email.Contains(tableInfo.SearchText) ||
                    tableInfo.SearchText == ""
                );

            IEnumerable<ApplicationUser> users = null;

            switch (tableInfo.SortBy)
            {
                case SortBy.Id:
                    users = tableInfo.SortDirection == SortDirection.Ascending
                        ? usersQuery.OrderBy(u => u.Id)
                        : usersQuery.OrderByDescending(u => u.Id);
                    break;
                case SortBy.UserName:
                    users = tableInfo.SortDirection == SortDirection.Ascending
                        ? usersQuery.OrderBy(u => u.UserName)
                        : usersQuery.OrderByDescending(u => u.UserName);
                    break;
                case SortBy.Email:
                    users = tableInfo.SortDirection == SortDirection.Ascending
                        ? usersQuery.OrderBy(u => u.Email)
                        : usersQuery.OrderByDescending(u => u.Email);
                    break;
            }

            tableInfo.PagingInfo.AllItems = users.Count();
            tableInfo.PagingInfo.AllPages =
                (int)Math.Ceiling((double)tableInfo.PagingInfo.AllItems / tableInfo.PagingInfo.ItemsPerPage);

            return users
                .Skip((tableInfo.PagingInfo.CurrentPage - 1) * tableInfo.PagingInfo.ItemsPerPage)
                .Take(tableInfo.PagingInfo.ItemsPerPage)
                .ToList();
        }
    }
}