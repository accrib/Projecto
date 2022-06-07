using _01_DAL;
using _02_WebService.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_WebService.Models.DataManager
{
    public class Linha_Fatura_DataManager : IDataRepository<Linha_Fatura>
    {
        readonly BD _linhaFaturaContext;

        public Linha_Fatura_DataManager(BD context)
        {
            _linhaFaturaContext = context;
        }

        public IEnumerable<Linha_Fatura> GetAll()
        {
            return _linhaFaturaContext.Linhas_Fatura.ToList();
        }

        public void Add(Linha_Fatura entity)
        {
            _linhaFaturaContext.Linhas_Fatura.Add(entity);
            _linhaFaturaContext.SaveChanges();
        }

        public void Delete(Linha_Fatura entity)
        {
            _linhaFaturaContext.Linhas_Fatura.Remove(entity);
            _linhaFaturaContext.SaveChanges();
        }

        public Linha_Fatura Get(long id)
        {
            return _linhaFaturaContext.Linhas_Fatura
                  .FirstOrDefault(e => e.ID == id);
        }

        public void Update(Linha_Fatura dbEntity, Linha_Fatura entity)
        {
            dbEntity.Produto = entity.Produto;
            dbEntity.Quantidade_Produto = entity.Quantidade_Produto;
            dbEntity.Preco_Produto = entity.Preco_Produto;

            _linhaFaturaContext.SaveChanges(); ;
        }
    }
}
