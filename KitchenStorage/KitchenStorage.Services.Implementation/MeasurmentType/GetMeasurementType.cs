using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenStorage.Services.Implementation.MeasurmentType
{
    public class GetMeasurementType : IGetMeasurementType
    {
        private readonly IBaseQuery<MeasurementType> _query;

        public GetMeasurementType(IBaseQuery<MeasurementType> query)
        {
            _query = query;
        }

        public async Task<MeasurementType?> FindByIdAsync(Guid id)
            => await _query.GetAsync(id);

        public async Task<IEnumerable<MeasurementType>> TypesAsync()
            => await _query.GetAllAsync();
    }
}
