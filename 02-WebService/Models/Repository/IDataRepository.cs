using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_WebService.Models.Repository
{
    /*   
     Esta inferface vai ser usada no API Controller para fazer a comunicação com a Base de Dados.
     Para tal, tem de ser implementada uma class que faça essa implementação. As class encontram-se na pasta DataManager.
    */

    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Update(TEntity dbEntity, TEntity entity);
        void Delete(TEntity entity);
    }

}
