﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Data.Contracts
{
    public interface IRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        Task<string> GetAsync(string uri);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
