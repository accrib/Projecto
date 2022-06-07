using _01_DAL;
using _02_WebService.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_WebService.Models.DataManager
{
    public class Fatura_DataManager : IDataRepository<Fatura>
    {
        readonly BD _faturaContext;

        public Fatura_DataManager(BD context)
        {
            _faturaContext = context;
        }

        public IEnumerable<Fatura> GetAll()
        {
            return _faturaContext.Faturas.ToList();
        }
        public void Add(Fatura entity)
        {
            _faturaContext.Faturas.Add(entity);
            _faturaContext.SaveChanges();
        }

        public void Delete(Fatura entity)
        {
            _faturaContext.Faturas.Remove(entity);
            _faturaContext.SaveChanges();
        }

        public Fatura Get(long id)
        {
            return _faturaContext.Faturas
                  .FirstOrDefault(e => e.ID == id);
        }

        public void Update(Fatura dbEntity, Fatura entity)
        {
            dbEntity.Data_Fatura = entity.Data_Fatura;
            dbEntity.Empregado = entity.Empregado;
            dbEntity.Lista_Linhas_Faturas = entity.Lista_Linhas_Faturas;
            dbEntity.Preco_Final = entity.Preco_Final;

            _faturaContext.SaveChanges();
        }
    }
}
