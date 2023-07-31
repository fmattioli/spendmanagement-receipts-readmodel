﻿using Application.Mappers;
using Domain.Interfaces;
using MediatR;
using Web.Contracts.Category;

namespace Application.Queries.Category.GetCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryEntity = await categoryRepository.FindOneAsync(x => x.Id == request.ReceiptId);
            var response = categoryEntity.ToCategoryResponse();
            return response;
        }
    }
}