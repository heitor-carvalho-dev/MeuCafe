# ☕ MeuCafé

API REST inspirada no [BuyMeACoffee](https://buymeacoffee.com) — uma plataforma onde criadores de conteúdo podem receber doações simbólicas de seus apoiadores, no formato de "comprar um café".

> 🚧 Projeto em desenvolvimento ativo.

---

## 🧱 Arquitetura

O projeto foi construído seguindo os princípios de **Clean Architecture**, com separação clara de responsabilidades entre as camadas:

```
MeuCafé/
├── Domain/           # Entidades, interfaces e regras de negócio (core)
├── Application/      # Use cases, DTOs
├── Infrastructure/   # Implementações de repositório, EF Core, mapeamentos
├── API/              # Controllers, middlewares e configuração da aplicação
└── UI/               # (em pausa)
```

A dependência flui sempre de fora pra dentro — as camadas externas conhecem as internas, nunca o contrário.

---

## 🛠️ Tecnologias

- **ASP.NET Core** — framework principal da API
- **Entity Framework Core** — ORM para mapeamento objeto-relacional
- **PostgreSQL** — banco de dados relacional
- **Middleware customizado** — tratamento centralizado de erros

---

## 🚀 Funcionalidades

| Use Case | Descrição |
|---|---|
| Criar usuário | Cadastro de criador de conteúdo na plataforma |
| Deletar usuário | Remoção de conta e dados associados |
| Listar usuários | Listagem de todos os perfis cadastrados |
| Warmup | Rota de aquecimento para verificar disponibilidade da API |

---

## ⚙️ Como rodar localmente

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Configuração

1. Clone o repositório:
```bash
git clone https://github.com/CupOfCakes/MeuCafe.git
cd MeuCafe
```

2. Configure a string de conexão no `appsettings.json` (pasta `API/`):
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=meucafe;Username=seu_usuario;Password=sua_senha"
}
```

3. Aplique as migrations:
```bash
dotnet ef database update --project Infrastructure --startup-project API
```

4. Rode a aplicação:
```bash
dotnet run --project API
```

A API estará disponível em `https://localhost:5001`.

---

## 📌 Observações

- A camada `UI` foi pausada — o foco do projeto é a API.
- Novas funcionalidades (doações, perfis públicos) estão planejadas para as próximas versões.
