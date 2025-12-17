using Hiquotroca.API.DTOs.Posts.Requests;
using MediatR;

namespace Hiquotroca.API.Application.UseCases.Posts.Commands.CreatePost;

public record CreatePostCommand(
    string Title,
    string Description,
    long UserId,
    long ActionTypeId,
    long CategoryId,
    long SubCategoryId,
    List<string> Images,

    // Location fields
    string? Location_Address,
    string Location_City,
    string? Location_PostalCode,
    string Location_Country,
    double? Location_Latitude,
    double? Location_Longitude,
    int? Location_DeliveryRadiusKm,

    // AdditionalInfo fields
    string? AdditionalInfo_Elements,
    string? AdditionalInfo_Caracteristics,
    int? AdditionalInfo_Duration
) : IRequest;