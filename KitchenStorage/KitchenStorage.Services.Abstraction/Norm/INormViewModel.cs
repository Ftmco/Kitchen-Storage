namespace KitchenStorage.Services.Abstraction
{
    public interface INormViewModel
    {
        NormViewModel CreateNormViewModel(Norm norm);

        IEnumerable<NormViewModel> CreateNormViewModel(IEnumerable<Norm> norms);
    }
}
