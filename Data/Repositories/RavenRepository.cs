using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessEntities;
using Common;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Client.Document;
using Raven.Abstractions.Exceptions;

namespace Data.Repositories
{
    [AutoRegister]
    public class RavenRepository<T> : IRepository<T> where T : IdObject
    {
        private readonly IAsyncDocumentSession _documentSession;

        public RavenRepository(IAsyncDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task SaveAsync(T entity)
        {
            try
            {
                await _documentSession.StoreAsync(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DeleteAsync(T entity)
        {
            try
            {
                _documentSession.Delete(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetAsync(Guid id)
        {
            try
            {
                var entity = await _documentSession.LoadAsync<T>(id);
                return entity;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected async Task DeleteAllAsync<TIndex>() where TIndex : AbstractIndexCreationTask<T>
        {
            await Task.Run(() =>
            {
                _documentSession.Advanced.DocumentStore.DatabaseCommands
                    .DeleteByIndex(typeof(TIndex).Name, new IndexQuery());
            });
        }


    }

}

