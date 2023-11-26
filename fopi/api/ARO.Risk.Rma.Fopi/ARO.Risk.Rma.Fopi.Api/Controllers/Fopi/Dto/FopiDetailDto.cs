using ARO.Risk.Rma.Fopi.Api.Controllers.Common;
using ARO.Risk.Rma.Fopi.Domain.Common;
using ARO.Risk.Rma.Fopi.Domain.Fopi;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ARO.Risk.Rma.Fopi.Api.Controllers.Fopi.Dto
{
    public class FopiDetailDto : FopiDetail, ICanValidateDto
    {
        public bool validate()
        {
            var manager = new ErrorManager<FopiDetailDto>(this);
            this.Error = new Dictionary<string, ISet<string>>();

            if (this.FuncId <= 0)
                manager.AddPropertyError("FuncId", ErrorCode.ApiLayer | ErrorCode.IdShouldBeSet);

            if (string.IsNullOrWhiteSpace(this.PayoffName))
                manager.AddPropertyError("PayoffName", ErrorCode.ApiLayer | ErrorCode.NotSet);

            if (string.IsNullOrWhiteSpace(this.Context))
                manager.AddPropertyError("Context", ErrorCode.ApiLayer | ErrorCode.NotSet);

            if (string.IsNullOrWhiteSpace(this.Documents))
                manager.AddPropertyError("CDocumentsontext", ErrorCode.ApiLayer | ErrorCode.NotSet);

            return this.Error.Keys.Count > 0;
        }
    }


public static class FopiDetailDtoEndpoints
{
	public static void MapFopiDetailDtoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/FopiDetailDto").WithTags(nameof(FopiDetailDto));

        group.MapGet("/", () =>
        {
            return new [] { new FopiDetailDto() };
        })
        .WithName("GetAllFopiDetailDtos")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new FopiDetailDto { ID = id };
        })
        .WithName("GetFopiDetailDtoById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, FopiDetailDto input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateFopiDetailDto")
        .WithOpenApi();

        group.MapPost("/", (FopiDetailDto model) =>
        {
            //return TypedResults.Created($"/api/FopiDetailDtos/{model.ID}", model);
        })
        .WithName("CreateFopiDetailDto")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new FopiDetailDto { ID = id });
        })
        .WithName("DeleteFopiDetailDto")
        .WithOpenApi();
    }
}}
