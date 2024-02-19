﻿using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract;

public interface IDataService
{
    Task<BaseResponse<IEnumerable<DataModel>>> GetAllData();
    Task<BaseResponse<DataModel>> AddData(DataModel model);
    Task<BaseResponse<DataModel>> UpdateData(DataModel model);
    Task<BaseResponse<DataModel>> GetDataById(string id);
    Task<BaseResponse<IEnumerable<CaseModel>>> GetCase();
}