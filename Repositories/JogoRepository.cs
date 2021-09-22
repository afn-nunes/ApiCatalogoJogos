using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Jogo{ Id= Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome= "The king of figther 97", Preco= 10.0, Produtora="Konami"} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Jogo{ Id= Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome= "Metal slug 4", Preco= 6.0, Produtora="KONAMI"} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Jogo{ Id= Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome= "Street Fighter IY", Preco= 16.0, Produtora="CAPCOM"} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Jogo{ Id= Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome= "College Football USA '97 - The Road to New Orleans", Preco= 3.99, Produtora="SEGA"} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Jogo{ Id= Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome= "Donkey Kong Country 3 - Dixie Kong's Double Trouble", Preco= 5.99, Produtora="Nintendo"} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Jogo{ Id= Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Nome= "The king of figther 97", Preco= 29.90, Produtora="Nintendo"} }
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //
        }
    }
}
