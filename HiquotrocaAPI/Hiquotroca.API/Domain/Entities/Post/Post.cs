using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Chat;
using Hiquotroca.API.Domain.Entities.Post.ValueObjects;
using Hiquotroca.API.Domain.Entities.Users;
using static System.Net.Mime.MediaTypeNames;

namespace Hiquotroca.API.Domain.Entities.Post
{
    public class Post : BaseEntity
    {
        //Post Basic Info
        public string Title { get; private set; } = string.Empty!;
        public string Description { get; private set; } = string.Empty!;
        public long UserId { get; private set; } 
        public bool IsActive { get; private set; } = true;
        public int? Duration { get; private set; }  //Duration in days
        public List<string> Images { get; private set; } = new List<string>();

        public PostAdditionalData AdditionalData { get; private set; }
        public PostTaxonomy PostTaxonomyData { get; private set; }
        public PostLocation Location { get; private set; }
        public List<long> Chats { get; private set; } = new List<long>();

        private Post() { }

        public Post(string title, string description, long userId, List<string> images, PostTaxonomy taxonomy, PostLocation postLocation, PostAdditionalData additionalData)
        {
            Title = title;
            Description = description;
            UserId = userId;

            IsActive = true;

            PostTaxonomyData = taxonomy;
            Location = postLocation;
            AdditionalData = additionalData;

            if(images == null && images.Count < 1)
            {
                throw new ArgumentNullException("At least one image is required for a post");
            }

            Images = images;
        }

        private Post UpdatePostBasicInfo(string title, string description)
        {
            if(string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("Title cannot be empty");
            if(string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Description cannot be empty");

            Title = title;
            Description = description;

            return this;
        }

        public Post UpdatePostLocation(string? address, string? city, string? postalCode, long countryId, double? latitude, double? longitude, int? deliveryRadiusKm)
        {
            var postLocation = new PostLocation(address, city, postalCode, countryId, latitude, longitude, deliveryRadiusKm);
            Location = postLocation;
            return this;
        }

        public Post UpdatePostAdditionalInfo(string? elements, string? caracteristics, int? duration, bool? isFeatured)
        {
            AdditionalData = new PostAdditionalData(elements, caracteristics, duration, isFeatured ?? this.AdditionalData.IsFeatured);
            return this;
        }

        public Post AddImages(List<string> images)
        {
            images.ForEach(image =>
            {
                if (!string.IsNullOrWhiteSpace(image) && !Images.Contains(image))
                {
                    Images.Add(image);
                }
            });

            return this;
        }

        public Post RemoveImages(List<string> images)
        {
            if(images.Count >= Images.Count)
                throw new InvalidOperationException("A post must have at least one image.");
            
            images.ForEach(image =>
            {
                if (!string.IsNullOrWhiteSpace(image) && Images.Contains(image))
                {
                    Images.Remove(image);
                }
            });

            return this;
        }

        public Post IncrementViewCounter(int viewsNumber = 1)
        {
            AdditionalData.IncrementViewCounter(viewsNumber);
            return this;
        }
    }
}
