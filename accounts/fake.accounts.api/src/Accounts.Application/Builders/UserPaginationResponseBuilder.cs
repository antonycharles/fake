using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Mappers.UserMappers;
using Accounts.Core.Builders;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.DTO.Responses.User;
using Accounts.Core.Entities;

namespace Accounts.Application.Builders
{
    public class UserPaginationResponseBuilder : IUserPaginationResponseBuilder
    {
        public UserPaginationResponse Build(PaginationRequest paginationRequest, IEnumerable<User> users, int totalRecords)
        {
            var TotalPages = GetTotalPages(paginationRequest.Take, totalRecords);

            return new UserPaginationResponse
            {
                Data = users.Select(s => s.ToResponse()).ToList(),
                Pagination = new PaginationResponse
                {
                    Search = paginationRequest.Search,
                    TotalRecords = totalRecords,
                    TotalPages = TotalPages,
                    CurrentPage = paginationRequest.CurrentPage,
                    NextPage = GetNextPage(paginationRequest, TotalPages),
                    PrevPage = GetPrevPage(paginationRequest)
                }
            };
        }

        private static int GetTotalPages(int take, int totalRecords)
        {
            return Convert.ToInt32(Math.Ceiling((decimal)totalRecords / take));
        }

        private int? GetNextPage(PaginationRequest paginationRequest, int TotalPages){
            var nextPage = paginationRequest.CurrentPage+1;
            return TotalPages >= nextPage ? nextPage : null;
        }

        private int? GetPrevPage(PaginationRequest paginationRequest){
            var prevPage = paginationRequest.CurrentPage-1;
            return prevPage > 0 ? prevPage : null;
        }
    }
}