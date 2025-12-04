using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Chat;
using Hiquotroca.API.Domain.Entities.Users;
using static System.Net.Mime.MediaTypeNames;

namespace Hiquotroca.API.Domain.Entities.Posts
{
    public class Post : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public long UserId { get; private set; }
        public long ActionTypeId { get; private set; }
        public long CategoryId { get; private set; }
        public long SubcategoryId { get; private set; }
        public PostLocation? Location { get; private set; }
        public bool IsFeatured { get; private set; } = false; // Featured/Premium posts
        public bool IsActive { get; private set; } = true;
        public string? Elements { get; private set; }
        public string? Caracteristics { get; private set; }
        public int ViewCounter { get; private set; } = 0;
        public int? Duration { get; private set; }  //Duration in days


        public virtual List<PostImage>? Images { get; private set; } = new List<PostImage>();
        public List<long> Chats { get; private set; } = new List<long>();

        private Post() { }

        public Post(string title, string? description, long userId, long actionTypeId, long categoryId, long subcategoryId, string? elements, string? caracteristics, int? duration)
        {
            Title = title;
            Description = description;
            UserId = userId;
            ActionTypeId = actionTypeId;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            Elements = elements;
            Caracteristics = caracteristics;
            Duration = duration;
            IsActive = true;
            IsFeatured = false;
        }

        public Post UpdatePost(string title, string? description, long actionTypeId, long categoryId, PostLocation? location)
        {
            Title = title;
            Description = description;
            ActionTypeId = actionTypeId;
            CategoryId = categoryId;
            Location = location;
            return this;
        }

        public Post UpsertPostLocation(string? address, string? city, string? postalCode, long? countryId, double? latitude, double? longitude, int? deliveryRadiusKm)
        {
            var postLocation = new PostLocation(address, city, postalCode, countryId, latitude, longitude, deliveryRadiusKm);
            Location = postLocation;
            return this;
        }

        public Post Activate()
        {
            IsActive = true;
            return this;
        }

        public Post Deactivate()
        {
            IsActive = false;
            return this;
        }

        public Post SetFeatured()
        {
            IsFeatured = true;
            return this;
        }

        public Post UnsetFeatured()
        {
            IsFeatured = false;
            return this;
        }

        public Post SetElements(string elements)
        {
            Elements = elements;
            return this;
        }

        public Post SetCaracteristics(string caracteristics)
        {
            Caracteristics = caracteristics;
            return this;
        }

        public Post AddImages(List<PostImage> images)
        {
            Images ??= new List<PostImage>(); //grantes that the Images list is not null

            bool isPrimaryPriorityAvaiable = images.Exists(img => img.IsPrimary);

            foreach (var image in images)
            {
                var url = image.Url;
                var isPrimary = image.IsPrimary;

                PostImage postImage;
                if(isPrimary)
                {
                    postImage = new PostImage(url, isPrimary: true);
                }
                else
                {
                    postImage = new PostImage(url);
                }

                Images!.Add(postImage);
            }
            return this;
        }

        public Post RemoveImages(List<PostImage> images)
        {
            foreach (var image in images)
            {
                if (Images.Contains(image))
                {
                    Images.Remove(image);
                }   
            }
            return this;
        }

        public Post UpdateViewCounter(int viewsNumber = 1)
        {
            ViewCounter += viewsNumber;
            return this;
        }
    }
}
