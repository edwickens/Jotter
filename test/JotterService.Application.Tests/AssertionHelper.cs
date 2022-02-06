using JotterService.Application.Features;
using JotterService.Domain;

namespace JotterService.Application.Tests;

public static class AssertionHelper
{
    public static bool EntityMatchesResponse(Password entity, GetAllPasswords.Response response)
    {
        if (entity is null)
            return false;
        if (response is null)
            return false;

        if (!entity.Id.Equals(response.Id))
            return false;
        if (!entity.UserId.Equals(response.UserId))
            return false;
        if (entity.Url != response.Url)
            return false;
        if (entity.Title != response.Title)
            return false;
        if (entity.Description != response.Description)
            return false;
        if (entity.Username != response.Username)
            return false;
        if (entity.CustomerNumber != response.CustomerNumber)
            return false;

        return true;
    }
}
