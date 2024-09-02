using Lista4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lista4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        public static List<Pessoa> pessoas = [];

        [HttpPost]
        public IActionResult AdicionarPessoa(Pessoa pessoa)
        {
            if (string.IsNullOrEmpty(pessoa.Cpf))
            {
                return BadRequest("O CPF não pode ser vazio");
            }

            var pessoaExiste = pessoas.FirstOrDefault(p => p.Cpf == pessoa.Cpf);

            if (pessoaExiste is not null)
            {
                return BadRequest("Já existe uma pessoa cadastrada com esse CPF");
            }

            pessoas.Add(pessoa);

            return Created(string.Empty, "Pessoa cadastrada com sucesso");
        }

        [HttpPut]
        public IActionResult AtualizarPessoa(Pessoa pessoa)
        {
            var pessoaAtualizada = pessoas.FirstOrDefault(p => p.Cpf == pessoa.Cpf);

            if (pessoaAtualizada is null)
            {
                return BadRequest("Não existe nenhuma pessoa com esse CPF");
            }

            pessoaAtualizada.Nome = pessoa.Nome;
            pessoaAtualizada.Peso = pessoa.Peso;
            pessoaAtualizada.Altura = pessoa.Altura;

            return Ok("Pessoa atualizada com sucesso");
        }

        [HttpDelete]
        public IActionResult RemoverPessoa(string cpf)
        {
            var pessoaRemover = pessoas.FirstOrDefault(p => p.Cpf == cpf);

            if (pessoaRemover is null)
            {
                return BadRequest("Não existe nenhuma pessoa com esse CPF");
            }

            pessoas.Remove(pessoaRemover);

            return Ok("Pessoa removida com sucesso");
        }

        [HttpGet]
        public IActionResult GetPessoas()
        {
            return Ok(pessoas);
        }

        [HttpGet]
        [Route("getByCpf")]
        public IActionResult GetByCpf(string cpf)
        {
            var pessoaPesquisada = pessoas.FirstOrDefault(p => p.Cpf == cpf);

            if (pessoaPesquisada is null)
            {
                return BadRequest("Não existe nenhuma pessoa com esse CPF");
            }

            return Ok(pessoaPesquisada);
        }

        [HttpGet]
        [Route("getByImc")]
        public IActionResult GetByImc()
        {
            var listaImcBom = new List<Pessoa>();

            foreach (var pessoa in pessoas)
            {
                var imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);

                if (imc >= 18 && imc <= 24)
                    listaImcBom.Add(pessoa);
            }

            if (listaImcBom.Count == 0)
            {
                return Ok("Nenhuma pessoa com IMC bom foi encontrada");
            }

            return Ok(listaImcBom);
        }

        [HttpGet]
        [Route("getByNome")]
        public IActionResult GetByNome(string nome)
        {
            var pessoaPesquisada = pessoas.FirstOrDefault(p => p.Nome == nome);

            if (pessoaPesquisada is null)
            {
                return BadRequest("Não existe nenhuma pessoa com esse nome");
            }

            return Ok(pessoaPesquisada);
        }
    }
}
