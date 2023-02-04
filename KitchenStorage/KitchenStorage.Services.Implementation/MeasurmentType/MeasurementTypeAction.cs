using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace KitchenStorage.Services.Implementation.MeasurmentType
{
    public class MeasurementTypeAction : IMeasurementTypeAction
    {
        private readonly IBaseQuery<MeasurementType> _query;
        private readonly IBaseCud<MeasurementType> _action;

        public MeasurementTypeAction
            (IBaseQuery<MeasurementType> query,
            IBaseCud<MeasurementType> action)
        {
            _query = query;
            _action = action;
        }

        public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> CreateAsync(UpsertMeasurementTypeViewModel upsert)
        {
            MeasurementType newMeasurement = new()
            {
                Name = upsert.Name,
            };

            return await _action.InsertAsync(newMeasurement) ?
                        newMeasurement : MeasurementTypeActionStatus.Failed;
        }

        public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> DeleteAsync(Guid id)
        {
            MeasurementType? measurementType = await _query.GetAsync(id);
            if (measurementType is null)
                return MeasurementTypeActionStatus.NotFound;

            measurementType.Status = (byte)EntityState.Deleted;
            return await _action.UpdateAsync(measurementType) ?
                        measurementType : MeasurementTypeActionStatus.Failed;
        }

        public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> UpdateAsync(UpsertMeasurementTypeViewModel upsert)
        {
            var measurement = await _query.GetAsync(upsert.Id);
            if (measurement is null)
                return MeasurementTypeActionStatus.NotFound;

            measurement.Name = upsert.Name;
            return await _action.UpdateAsync(measurement) ?
            measurement : MeasurementTypeActionStatus.Failed;
        }

        public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> UpsertAsync(UpsertMeasurementTypeViewModel upsert)
                => upsert.Id is null
                ? await CreateAsync(upsert)
                : await UpdateAsync(upsert);
    }
}
