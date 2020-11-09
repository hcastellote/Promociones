using Helpers;
using Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Repository
{
    public class MongoDBRepository<T> : IRepositoryBase<T> where T : class
    {
        private MongoDatabase database;
        private MongoCollection<T> collection;

        public bool BsonDiscriminator { get; private set; }

        public MongoDBRepository()
        {
            GetDatabase();
            GetCollection();
        }

        public IEnumerable<T> GetAll()
        {
            return collection.FindAllAs<T>().ToList();
        }
        public void Insert(T entity)
        {
            collection.Insert(entity);
        }

        public void Update(T entity)
        {
            collection.Save(entity);
        }

        public void Delete(T entity)
        {
            var bson = entity.ToBsonDocument().GetValue("_id");
            collection
           .Remove(Query.EQ("_id", bson));
        }

        public IList<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return collection
            .AsQueryable<T>()
            .Where(predicate.Compile())
            .ToList();
        }

        public T Get(Guid id)
        {
            return collection.FindOne(Query.EQ("id", id));
        }

        public T Get(int id)
        {
            return collection.FindOne(Query.EQ("id", id));
        }

        public T Get(string id)
        {
            return collection.FindOne(Query.EQ("_id", id));
        }

        public bool Exists(Guid id)
        {
            return collection.FindAs<T>(Query.EQ("id", id)).Count() > 0;
        }

        public bool Exists(int id)
        {
            return collection.FindAs<T>(Query.EQ("id", id)).Count() > 0;
        }

        public bool Exists(string id)
        {
            return collection.FindAs<T>(Query.EQ("id", id.ToUpper())).Count() > 0;
        }

       

        public int CollectionSize()
        {
            return (int)collection.FindAllAs<T>().Count();
        }

        #region Private Helper Methods  
        private void GetDatabase()
        {
            var client = new MongoClient("mongodb+srv://hcastellote:lerolero1@clusterpromociones.pfjbb.mongodb.net/Promociones?retryWrites=true&w=majority"); //TODO
            var server = client.GetServer();

            database = server.GetDatabase("Promociones");
        }
        private void GetCollection()
        {
            var type = typeof(T);
            var discriminator = (BsonDiscriminatorAttribute)type.GetCustomAttribute(typeof(BsonDiscriminatorAttribute));
            collection = database.GetCollection<T>(discriminator.Discriminator);
        }

        IEnumerable<T> IRepositoryBase<T>.GetListadoPromocionesVigentes()
        {
            var queryConditions = new List<IMongoQuery>();
            queryConditions.Add(Query.LTE("fecha_inicio", DateTime.Now));
            queryConditions.Add(Query.GTE("fecha_fin", DateTime.Now));

            return collection.FindAs<T>(Query.And(queryConditions.ToArray())).ToList();
        }

        IEnumerable<T> IRepositoryBase<T>.GetListadoPromocionesVigentesFecha(DateTime pFecha)
        {

            var queryConditions = new List<IMongoQuery>();
            queryConditions.Add(Query.LTE("fecha_inicio", pFecha));
            queryConditions.Add(Query.GTE("fecha_fin", pFecha));

            return collection.FindAs<T>(Query.And(queryConditions.ToArray())).ToList();
        }

        IEnumerable<T> IRepositoryBase<T>.GetListadoPromocionesVigentesVenta(string pMedioPago, string pBanco, IEnumerable<string> pCategoriaProducto)
        {
            var queryConditions = new List<IMongoQuery>();
            var arraymediopago = new BsonArray(new[] { pMedioPago });
            queryConditions.Add(Query.In("medios_de_pago", arraymediopago));

            var arrayBanco = new BsonArray(new[] { pBanco });
            queryConditions.Add(Query.In("bancos", arrayBanco));

            var array = new BsonArray(pCategoriaProducto.ToArray());
            queryConditions.Add(Query.In("categorias_productos", array));

            return collection.FindAs<T>(Query.And(queryConditions.ToArray())).ToList();
        }

        public void ModificaVigenciaPromocion(Guid pId, DateTime pFechaInicio, DateTime pFechaFin)
        {
            var queryConditions = new List<IMongoQuery>();
            queryConditions.Add(Query.GTE("fecha_inicio", pFechaInicio));
            queryConditions.Add(Query.LTE("fecha_fin", pFechaFin));

            collection.Update(Query.EQ("id", pId), Update<Promocion>.Set(x => x.FechaInicio, pFechaInicio).Set(x => x.FechaFin, pFechaFin));

        }

   

        public bool ValidarSolapamientoPromociones(Guid pId, IEnumerable<string> pMedioPago, IEnumerable<string> pBanco, IEnumerable<string> pCategoriaProducto, DateTime pFechaInicio, DateTime pFechaFin)
        {
            var queryConditions = new List<IMongoQuery>();
            var arraymediopago = new BsonArray(new[] { pMedioPago });
            queryConditions.Add(Query.In("medios_de_pago", arraymediopago));

            var arrayBanco = new BsonArray(new[] { pBanco });
            queryConditions.Add(Query.In("bancos", arrayBanco));

            var array = new BsonArray(pCategoriaProducto.ToArray());
            queryConditions.Add(Query.In("categorias_productos", array));

            queryConditions.Add(Query.GTE("fecha_inicio", pFechaInicio));
            queryConditions.Add(Query.LTE("fecha_fin", pFechaFin));

            queryConditions.Add(Query.NE("id", pId));
            return collection.FindAs<T>(Query.And(queryConditions.ToArray())).ToList().Count > 0;
        }

        #endregion
    }
}