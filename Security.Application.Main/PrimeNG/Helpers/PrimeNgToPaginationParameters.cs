using Security.Application.Dto.Base.PrimeNG;
using Security.Transversal.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Security.Application.MainModule.PrimeNG.Helpers
{
    public class PrimeNgToPaginationParameters<TDto> where TDto : class
    {
        public static PaginationParametersDto<TDto> Convert(PrimeTableDto primeTable)
        {
            var filter = new List<ColumnsFilter>();

            if (primeTable.Filters != null)
            {
                filter = primeTable.Filters
                    .Where(p => !string.IsNullOrWhiteSpace(p.Value.Value) && p.Key != "global")
                    .Select(p => new ColumnsFilter
                    {
                        Field = p.Key,
                        Value = p.Value.Value.Trim(),
                        Operator = p.Value.MatchMode
                    }).ToList();
            }

            var filterParameterDto = new PaginationParametersDto<TDto>
            {
                Start = primeTable.First,
                AmountRows = primeTable.Rows,
                SortField = primeTable.SortField,
                SortType = primeTable.SortOrder == 1
                    ? SortTypePrimeNGEnum.Ascending
                    : SortTypePrimeNGEnum.Descending,
                WhereFilter = filter.Any() ? LambdaManager.ConvertToLambda<TDto>(filter) : null
            };

            return filterParameterDto;
        }
    }
}
