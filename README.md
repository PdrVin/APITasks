# API Tasks 📋

Bem-vindo ao repositório da **API Tasks**, um sistema desenvolvido para gerenciar tarefas de forma eficiente, modular e escalável. Este projeto aplica conceitos modernos de desenvolvimento, como **Clean Code**, **Orientação a Objetos**, e o padrão **Repository Pattern**, além de utilizar **Entity Framework** com suporte ao MySQL.

## 🔧 Tecnologias Utilizadas

- **ASP.NET Core**: Framework principal para a construção da API.
- **Microsoft.EntityFrameworkCore**: ORM para manipulação do banco de dados.
- **Pomelo.EntityFrameworkCore.MySQL**: Driver para conexão com o MySQL.
- **Swashbuckle.AspNetCore**: Ferramenta para documentação interativa com Swagger.
- **xUnit e Moq**: Frameworks utilizados para criação e execução de testes unitários.

## 📂 Estrutura do Projeto

A estrutura do projeto foi cuidadosamente planejada para seguir os princípios da **orientação a objetos** e **Clean Code**, garantindo modularidade e legibilidade.

### **Principais Diretórios**

- **Controllers**: Contém os controladores da API (`TasksController.cs`), responsáveis por lidar com as requisições HTTP.
- **Database**: Inclui a configuração do contexto do Entity Framework (`TaskContext.cs`).
- **DTO**: Contém os objetos de transferência de dados (`TaskDto.cs`) para facilitar a comunicação entre camadas.
- **Interfaces**: Define contratos com as interfaces base e específicas, como `IRepository`, `ITaskRepository` e `ITaskService`.
- **Models**: Modelos da aplicação, incluindo `TaskModel.cs` para representação das tarefas e `Errors` para manipulação de erros personalizados.
- **Repository**: Implementa o padrão Repository (`Repository.cs` e `TaskRepository.cs`) para centralizar a lógica de acesso ao banco de dados.
- **Services**: Contém `TaskService.cs`, que aplica as regras de negócio da aplicação.
- **Views**: Exemplos de arquivos HTTP (`APITasks.http`) para chamadas diretas durante o desenvolvimento.
- **Test**: Testes unitários utilizando `xUnit` e `Moq`, definidos no arquivos `TestController.cs` e `TestRepository.cs`.

---

## 🧪 Testes Automatizados

Os testes são implementados utilizando o framework **xUnit** para garantir a qualidade e o funcionamento do sistema. O projeto também usa o **Moq** para criar mocks das dependências, permitindo testes sem conexão com banco de dados.

### Métodos Testados

- **GetAllAsync**: Retorna a lista paginada de tarefas.
- **GetByIdAsync**: Busca uma tarefa específica pelo ID.
- **AddAsync**: Adiciona uma nova tarefa ao banco.
- **UpdateAsync**: Atualiza uma tarefa existente.
- **DeleteAsync**: Remove uma tarefa do banco, com verificação de ID inválido.

### Comandos para Executar os Testes

1. Certifique-se de ter o **xUnit** instalado.
2. Execute o seguinte comando no terminal:

```bash
dotnet test
```

---

## 🚀 Funcionalidades Implementadas

1. **CRUD de Tarefas**:
   - Adicionar, atualizar, listar e remover tarefas.
   - Validação de IDs inexistentes.

2. **Injeção de Dependência**:
   - Interfaces como `IRepository` e `ITaskService` são utilizadas para permitir alta flexibilidade e facilidade de manutenção.

3. **Padrões de Desenvolvimento**:
   - Aplicação do padrão **Repository** para abstração do acesso aos dados.
   - Utilização de **Clean Code** para melhorar a legibilidade e qualidade do código.

4. **Documentação Interativa**:
   - Integração com **Swagger** para facilitar o utilização da API.

---

## 🌟 Como Executar o Projeto

1. Clone este repositório:
   ```bash
   git clone <url-do-repositorio>
   cd API_Tasks
   ```

2. Configure a conexão com o banco no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=TasksDB;User=root;Password=yourpassword;"
   }
   ```

3. Execute as migrações para criar o banco:
   ```bash
   dotnet ef database update
   ```

4. Rode o projeto:
   ```bash
   dotnet run
   ```

5. Acesse a documentação no Swagger:
   - URL: `http://localhost:5000/swagger`

---

## 💡 Contribuindo

Sinta-se à vontade para abrir issues e pull requests. Toda contribuição é bem-vinda!
