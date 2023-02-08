namespace KitchenStorage.Services.Abstraction;

public interface INormViewModel
{
    Task<NormViewModel> CreateNormViewModelAsync(Norm norm);

    Task<IEnumerable<NormViewModel>> CreateNormViewModelAsync(IEnumerable<Norm> norms);
}
