using _01_DAL;
using _02_WebService.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace _02_WebService.Models.DataManager
{
    /*   
        Esta class vai implementar a interface IDataRepository, que se encontra na pasta Repository
        Esta class vai tratar de todas as operação feitas à Base de Dados, relacionadas com a class Empregado da bd
        O objectivo desta class é de separar a lógica dos controladores da API.
        Seguidamente tem de ser feita a injecção da dependência no Startup
    */

    public class Empregado_DataManager : IDataRepository<Empregado>
    {
        readonly BD _empregadoContext;

        public Empregado_DataManager(BD context)
        {
            _empregadoContext = context;
        }

        public IEnumerable<Empregado> GetAll()
        {

            return _empregadoContext.Empregados.ToList();
        }

        public void Add(Empregado entity)
        {
            _empregadoContext.Empregados.Add(entity);
            _empregadoContext.SaveChanges();
        }

        public void Delete(Empregado entity)
        {
            _empregadoContext.Empregados.Remove(entity);
            _empregadoContext.SaveChanges();
        }

        public Empregado Get(long id)
        {
            var resultado = _empregadoContext.Empregados.FirstOrDefault(e => e.ID == id);

            return resultado;
        }


        public void Update(Empregado dbEntity, Empregado entity)
        {
            dbEntity.Nome = entity.Nome;
            dbEntity.Numero_Empregado = entity.Numero_Empregado;
            dbEntity.Lista_Produtos = entity.Lista_Produtos;
            dbEntity.Lista_Faturas = entity.Lista_Faturas;
            dbEntity.EMail = entity.EMail;

            _empregadoContext.SaveChanges();
        }
    }
}
