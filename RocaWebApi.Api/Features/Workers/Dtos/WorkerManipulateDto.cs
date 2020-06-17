using System.ComponentModel.DataAnnotations;

namespace RocaWebApi.Api.Features.Workers
{
    public class WorkerManipulateDto
    {
        [Required(ErrorMessage = "É obrigatório informar o Nome")]
        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(20, ErrorMessage = "O telefone deve conter no máximo 20 caracteres")]
        public string Phone { get; set; }

        [MaxLength(200, ErrorMessage = "O endereço deve conter no máximo 200 caracteres")]
        public string Address { get; set; }
    }
}
