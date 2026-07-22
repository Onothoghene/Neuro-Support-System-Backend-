using Application.DTOs.Comments;
using Application.Features.Comment.Command;
using AutoMapper;
using Domain.Entities;

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
