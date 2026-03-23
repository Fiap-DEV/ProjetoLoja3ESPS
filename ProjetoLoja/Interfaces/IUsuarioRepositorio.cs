using ProjetoLoja.Models;

namespace ProjetoLoja.Interfaces
{
    public interface IUsuarioRepositorio
    {
        LoginViewModel Validar(string email, string senha);
    }
}
