using Fincompare.Application.Models.RateModel;

namespace Fincompare.Application.Contracts.Infrastructure
{
    public interface IExchangeRate
    {
        API_Obj Import(string baseCur);
    }
}
