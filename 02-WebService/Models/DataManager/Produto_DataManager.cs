using _01_DAL;
using _02_WebService.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _02_WebService.Models.DataManager
{
    public class Produto_DataManager : IDataRepository<Produto>
    {
        readonly BD _produtoContext;

        public Produto_DataManager(BD context)
        {
            _produtoContext = context;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _produtoContext.Produtos.ToList();
        }

        public void Add(Produto entity)
        {
            _produtoContext.Produtos.Add(entity);
            _produtoContext.SaveChanges();
        }

        public void Delete(Produto entity)
        {
            _produtoContext.Produtos.Remove(entity);
            _produtoContext.SaveChanges();
        }

        public Produto Get(long id)
        {
            return _produtoContext.Produtos
                  .FirstOrDefault(e => e.ID == id);
        }

        public void Update(Produto dbEntity, Produto entity)
        {
            dbEntity.Nome = entity.Nome;
            dbEntity.Empregado = entity.Empregado;
            dbEntity.Descricao_Produto = entity.Descricao_Produto;
            dbEntity.Faturas = entity.Faturas;
            dbEntity.Lista_Linhas_Faturas = entity.Lista_Linhas_Faturas;

            _produtoContext.SaveChanges(); ;
        }
    }
}
