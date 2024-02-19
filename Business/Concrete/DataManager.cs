using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;

namespace Business.Concrete;
public class DataManager : IDataService
{
    private readonly IDataDal _dataDal;

    public DataManager(IDataDal dataDal)
    {
        _dataDal = dataDal;
    }

    public async Task<BaseResponse<IEnumerable<DataModel>>> GetAllData()
    {
        var result = await _dataDal.GetAll();
        return new BaseResponse<IEnumerable<DataModel>>(result,true);
    }

    public async Task<BaseResponse<DataModel>> AddData(DataModel model)
    {
        await _dataDal.Insert(model);
        return new BaseResponse<DataModel>(model,true);
    }

    public async Task<BaseResponse<DataModel>> UpdateData(DataModel model)
    {
        await _dataDal.Update(model);
        return new BaseResponse<DataModel>(model,true);
    }

    public async Task<BaseResponse<DataModel>> GetDataById(string id)
    {
        var result = await _dataDal.GetById(id);
        return new BaseResponse<DataModel>(result,true);
    }

    public async Task<BaseResponse<IEnumerable<CaseModel>>> GetCase()
    {
        var result = await _dataDal.GetCase();
        return new BaseResponse<IEnumerable<CaseModel>>(result,true);
    }
}
