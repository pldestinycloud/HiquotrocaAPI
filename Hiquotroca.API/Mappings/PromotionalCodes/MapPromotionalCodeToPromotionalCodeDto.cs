using Hiquotroca.API.Domain.Entities;
using Hiquotroca.API.DTOs.PromotionalCode;

namespace Hiquotroca.API.Mappings.PromotionalCodes;

public static class MapPromotionalCodeToPromotionalCodeDto
{
    public static PromotionalCodeDto Map(PromotionalCode promoCode, PromotionalCodeDto promoCodeDto)
    {
        promoCodeDto.Id = promoCode.Id;
        promoCodeDto.Code = promoCode.Code;
        promoCodeDto.ExpiryDate = promoCode.ExpiryDate;
        promoCodeDto.IsActive = promoCode.IsActive;

        return promoCodeDto;
    }
}
