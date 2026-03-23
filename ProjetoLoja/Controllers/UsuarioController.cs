using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoLoja.Interfaces;
using ProjetoLoja.Models;
using System.Security.Claims;

namespace ProjetoLoja.Controllers
{
    public class UsuarioController : Controller
    {
        //criar o acesso ao banco de dados de usuarios
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        //construtor que prepara a ferramenta do banco de dados

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            //guarda as variaveis que criamos para ser usada depois
            _usuarioRepositorio = usuarioRepositorio;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel usermodel)
        {
            //verifica se o email e senha foram digitados corretamente
           if(!ModelState.IsValid) return View(usermodel);
           //Pergunta ao banco de dados se existe alguem com email e senha
           var usuario = _usuarioRepositorio.Validar(usermodel.Email, usermodel.Senha);
            //se o banco de dados encontrar o usuario , inicia a criação do cracha de acesso
            if(usuario != null)
            {
                //dados do cracha de identificação
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("NivelAcesso", usuario.Nivel),
                    new Claim("UsuarioId", usuario.Id.ToString())
                };
                //cria a identidade oficial do usuario baseada nos dados acima
                var identidade = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //espera para logar criando um cookie de segurança no navegador
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identidade),
                    // indica que a sessão acaba quando usuario fechar o navegador
                    new AuthenticationProperties { IsPersistent = false });
            }
            ModelState.AddModelError("", "Email ou senha inválidos");
            return View(usermodel);
        }
        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
