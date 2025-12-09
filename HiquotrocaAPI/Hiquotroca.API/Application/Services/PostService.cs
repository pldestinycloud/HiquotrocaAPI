using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.Domain.Entities.Posts;
using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Hiquotroca.API.Mappings.Posts;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Hiquotroca.API.Application.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;   
        }

        public async Task<BaseResult<List<PostDto>>> GetAllPostsAsync()
        {
            // Implementation goes here
            return new BaseResult<List<PostDto>>();
        }

        public async Task<BaseResult<PostDto>> GetPostByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        //Esta a retornar a entidade, ***MUDAR APOS CONFIRMAR QUE DADOS O FE QUER RECEBER****
        public async Task<BaseResult<Post>> CreatePostAsync(CreatePostRequest createPostRequest)
        {
            var post =  MapCreatePostRequestToNewPost.Map(createPostRequest);

            if (createPostRequest.ImageUrls != null && createPostRequest.ImageUrls.Any())
            {
                var postImages = createPostRequest.ImageUrls.Select(imgDto => new PostImage(imgDto.Url, imgDto.IsPrimary)).ToList();
                post = post.AddImages(postImages);
            }

            try
            {
                await _postRepository.AddAsync(post);
                return BaseResult<Post>.Ok(post);
            }
            catch (Exception ex)
            {
                return new Error(ErrorCode.Exception, "Something went Wrong");
            }
        }

        public async Task<BaseResult<PostDto>> UpdatePostAsync(long id, PostDto updatePostRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResult> DeletePostAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
