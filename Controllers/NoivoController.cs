using System.Reflection.Metadata.Ecma335;
using Data.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models.Pessoa.Noivo;

namespace Controllers
{
    [ApiController]
    public class NoivoController : ControllerBase
    {
        [HttpGet("v1/noivos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] ListaCasamentoDataContext context
        )
        {
            var noivos = await context.Noivos.ToListAsync();
            return Ok(noivos);

        }


        [HttpGet("v1/noivos/{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] ListaCasamentoDataContext context
        )
        {
            try
            {
                var noivos = await context.Noivos.FirstOrDefaultAsync(c => c.Id == id);
                return Ok(noivos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha Interna no Servidor");
            }
        }
        [HttpPost("v1/noivos")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorNoivoViewModel model,
            [FromServices] ListaCasamentoDataContext context
        )
        {
            try
            {
                var noivo = new Noivo
                {
                    Nome = model.Nome,
                    Aniversario = model.Aniversario,
                    Sexo = model.Sexo,
                    Familia = model.Familia,
                    Telefone = model.Telefone,
                    Email = model.Email.ToLower(),
                    Id = Guid.NewGuid()
                };
                await context.Noivos.AddAsync(noivo);
                await context.SaveChangesAsync();

                return Created($"v1/noivos{noivo.Id}", noivo);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Não foi possível adicionar um noivo(a)");
            }

        }

        [HttpDelete("v1/noivos/{id:Guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] ListaCasamentoDataContext context
        )
        {
            var deletarNoivo = context.Noivos.FirstOrDefault(c => c.Id == id);
            context.Remove(deletarNoivo);
            await context.SaveChangesAsync();
            return Ok($"Usuário {deletarNoivo.Nome} foi excluido com sucesso");
        }


        [HttpPut("v1/noivos/{id:Guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] EditorNoivoViewModel model,
            [FromServices] ListaCasamentoDataContext context
        )
        {
            try
            {
                var atualizaNoivo = await context.Noivos.FirstOrDefaultAsync(c => c.Id == id);
                if (atualizaNoivo == null)
                {
                    return NotFound();
                }

                atualizaNoivo.Nome = model.Nome;
                atualizaNoivo.Aniversario = model.Aniversario;
                atualizaNoivo.Sexo = model.Sexo;
                atualizaNoivo.Familia = model.Familia;
                atualizaNoivo.Telefone = model.Telefone;
                atualizaNoivo.Email = model.Email.ToLower();


                context.Update(atualizaNoivo);
                await context.SaveChangesAsync();

                return Ok(atualizaNoivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha Interna no Servidor");
            }
        }


    }
}


