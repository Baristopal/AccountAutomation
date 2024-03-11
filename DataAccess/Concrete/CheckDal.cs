﻿using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete;
public class CheckDal : ICheckDal
{
    private readonly INoSqlHelper _noSqlHelper;

    public CheckDal(INoSqlHelper noSqlHelper)
    {
        _noSqlHelper = noSqlHelper;
    }

    public async Task Add(CheckModel model)
    {
        await _noSqlHelper.InsertAsync(model.Id, model);
    }

    public async Task Update(CheckModel model)
    {
        await _noSqlHelper.UpdateAsync(model.Id, model);
    }

    public async Task<IEnumerable<CheckModel>> GetAll()
    {
        string query = "SELECT c.* FROM Data._default.Check as c WHERE c.isDeleted = false ORDER BY c.createdDate";
        var result = await _noSqlHelper.QueryAsync<CheckModel>(query);
        return result;
    }

    public async Task<CheckModel> GetById(string id)
    {
        var result = await _noSqlHelper.GetByIdAsync<CheckModel>(id);
        return result;
    }
}
