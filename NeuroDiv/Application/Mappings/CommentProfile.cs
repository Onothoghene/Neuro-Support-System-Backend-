using Application.DTOs.Comments;
using Application.DTOs.Organizations;
using Application.DTOs.OrganizationUserRoles;
using Application.DTOs.OrganizationUsers;
using Application.DTOs.OrganizationUsersInvite;
using Application.Features.Comment.Command;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<AddOrUpdateCommentCommand, Comments>();

            CreateMap<Comments, CommentVM>();

        }

    }
}
