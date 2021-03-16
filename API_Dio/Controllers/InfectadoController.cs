using API_Dio.Data.Collections;
using API_Dio.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Dio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDBContext _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDBContext mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoModel dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectado);
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult EditarInfectados([FromBody] InfectadoModel dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => _.DataNascimento == dto.DataNascimento),
                                            Builders<Infectado>.Update.Set("sexo", dto.Sexo));
            return Ok("Atualizado com sucesso.");
        }

        [HttpDelete("dataNascimento")]
        public ActionResult ApagarInfectados(DateTime dataNascimento)
        {
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.DataNascimento == dataNascimento));
            return Ok("Removido com sucesso.");
        }
    }
}
