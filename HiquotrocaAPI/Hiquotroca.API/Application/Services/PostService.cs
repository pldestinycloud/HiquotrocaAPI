using Hiquotroca.API.Application.Wrappers;
using Hiquotroca.API.Domain.Entities.Post;
using Hiquotroca.API.Domain.Entities.Post.ValueObjects;
using Hiquotroca.API.DTOs.Posts;
using Hiquotroca.API.DTOs.Posts.Requests;
using Hiquotroca.API.Infrastructure.Persistence.Repositories;
using Hiquotroca.API.Mappings.Posts;

namespace Hiquotroca.API.Application.Services
{
    public class PostService
    {
        private readonly PostRepository _postRepository;
        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            IEnumerable<Post> posts = await _postRepository.GetAllAsync();

            if (posts == null || !posts.Any())
                return new List<PostDto>();


            return posts.Select(post => MapPostToPostDto.Map(post, new PostDto())).ToList();
        }

        public async Task<PostDto?> GetPostByIdAsync(long id)
        {
            Post? post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;

            post.IncrementViewCounter();
            await _postRepository.UpdateAsync(post);

            return MapPostToPostDto.Map(post, new PostDto());
        }

        //Tentei partir isto em varios bocados para nao ter um construtor gigantesco, mas agora este metodo ta cheio de "mappers" o que tb n ta bonito,
        //vou deixar assim por agora, mas no futuro se calhar é melhor meter um builder ou algo do genero
        public async Task CreatePostAsync(CreatePostDto createPostDto)
        {
            var postLocation = new PostLocation(
                createPostDto.Location!.Address,
                createPostDto.Location.City,
                createPostDto.Location.PostalCode,
                countryId: createPostDto.Location.CountryId,
                createPostDto.Location.Latitude,
                createPostDto.Location.Longitude,
                createPostDto.Location.DeliveryRadiusKm);

            var postTaxonomy = new PostTaxonomy(
                createPostDto.ActionTypeId,
                createPostDto.CategoryId,
                createPostDto.SubCategoryId);

            var postAdditionalData = new PostAdditionalData(
                createPostDto.AdditionalInfo!.Elements,
                createPostDto.AdditionalInfo.Caracteristics,
                createPostDto.AdditionalInfo.Duration); 

            var post = new Post(
                createPostDto.Title,
                createPostDto.Description,
                createPostDto.UserId,
                createPostDto.Images,
                postTaxonomy,
                postLocation,
                postAdditionalData);

            await _postRepository.AddAsync(post);
        }

        //verificar quais sao os campos que podem ser alterados ou nao
        /*public async Task<BaseResult<PostDto>> UpdatePostAsync(long id, PostDto updatePostRequest)
        {
            //verificar quais sao os campos que podem ser alterados ou nao
            throw new NotImplementedException();
        }*/

        public async Task DeletePostAsync(long id)
        {
             var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                return;
            
            await _postRepository.DeleteAsync(post);
        }
    }
}
