﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OhMyDog_API.Model.Agendamento;
using System.Data.Odbc;
using static OhMyDog_API.Model.Parameters;
using static OhMyDog_API.Controllers.ConexaoController;
using System.Data;

namespace OhMyDog_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController : ControllerBase
    {
        [HttpGet]
        [Route("RetornarInformacoesBasicas")]
        public async Task<IActionResult> GetInformacoesBasicas()
        {
            try
            {
                AgendamentoPetShop agendamento;
                List<AgendamentoPetShop> agendamentosPet = new List<AgendamentoPetShop>();

                var table = ExecutaQuery("SELECT * FROM Agendamento");

                foreach (DataRow reader in table.Rows)
                {
                    agendamentosPet.Add(new AgendamentoPetShop
                    {
                        CodAgendamento = reader["CodAgendamento"].ToString(),
                        CodPet = reader["CodPet"].ToString(),
                        DataAgendamento = DateTime.Parse(reader["DataAgendamento"].ToString())
                    });
                }
                return Ok(agendamentosPet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("InserirNovoAgendamento")]
        public async Task<IActionResult> PostNovoAgendamento([FromBody] AgendamentoPetShop agendamento)
        {
            try
            {
                if (agendamento == null)
                    return BadRequest();

                ExecutaQuery($"INSERT INTO Agendamento (CodAgendamento, CodPet, DataAgendamento) VALUES ('{agendamento.CodAgendamento}', '{agendamento.CodPet}', '{agendamento.DataAgendamento}')");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Route("DeletarAgendamento/{codAgendamento}")]
        public async Task<IActionResult> DeletarAgendamento([FromRoute] string codAgendamento)
        {
            try
            {
                string queryString = $"SELECT codAgendamento FROM Agendamento WHERE codAgendamento = '{codAgendamento}'";
                var table = ExecutaQuery(queryString);

                if (table.Rows.Count == 0)
                    return BadRequest("Agendamento não encontrado!");

                ExecutaQuery($"DELETE FROM Agendamento WHERE codAgendamento ='{codAgendamento}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }


        [HttpPatch]
        [Route("AtualizarAgendamento")]
        public async Task<IActionResult> AtualizarAgendamento([FromBody] AgendamentoPetShop agendamento)
        {
            try
            {
                if (agendamento == null)
                    return BadRequest();

                var table = ExecutaQuery($"SELECT codAgendamento FROM Agendamento WHERE codAgendamento = '{agendamento.CodAgendamento}'");

                if (table.Rows.Count == 0)
                    return BadRequest("Agendamento não encontrado!");

                ExecutaQuery($"UPDATE Agendamento SET " +
                    $"DataAgendamento = '{agendamento.DataAgendamento}' " +
                    $"WHERE codAgendamento = '{agendamento.CodAgendamento}'");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
