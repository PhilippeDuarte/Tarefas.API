<head></head>
<body>
    <h2>Sistema Tarefas.API</h2>
    <h3>Sobre Tarefas.API</h3>
    <p>Este sistema faz parte do teste de recrutamento proposto pela Ponta Agro.</p>
    <p>O objetivo do teste é criar uma API para gerenciar tarefas e garantir a segurança de acesso através de autenticação e autorização de usuários.</p>
    <h3>Recursos usados no desenvolvimento</h3>
    <p>Este sistema foi desenvolvido usando o <b>Microsoft Visual Studio Community 2022 V17.4.4.</b>, usando a estrutura <b>ASP. Net Core 6.0</b>.</p>
    <p>Abaixo seguem os pacotes de dependências usados no desenvolvimento deste projeto:
        <ul>
            <li>Autenticação e Autorização</li>
            <ul>
                <li>Microsoft.AspNetCore.Authentication.JwtBearer (6.0.24)</li>
                <li>Microsoft.identityModel.Tokens.Jwt (6.33.0)</li>
            </ul>
            <li>Banco de Dados</li>
            <ul>
                <li>Microsoft.EntityFrameworkCore.Sqlite (7.0.13)</li>
                <li>Microsoft.EntityFrameworkCore.Design (7.0.13)</li>
            </ul>
            <li>Documentação e Manipulação da API</li>
            <ul>
                <li>Swashbuckle.AspNetCore (6.5.0)</li>
                <li>Swashbuckle.AspNetCore.Swagger (6.5.0)</li>
                <li>Swashbuckle.AspNetCore.SwaggerGen (6.5.0)</li>
                <li>Swashbuckle.AspNetCore.SwaggerUI (6.5.0)</li>
            </ul>
            <li>Testes Unitários </li>
            <ul>
                <li>xunit (2.4.2)</li>
                <li>xunit.runner.visualstudio (2.4.5)</li>
                <li>Moq (4.20.69)</li>
            </ul>
        </ul>
    </p>
    <h3>Banco de Dados</h3>
    <p>Foi decidido pelo uso do Sqlite por ser um banco de dados que poderia ser usado através do EF Core e ser acoplado ao sistema não havendo necessidades de aplicações externas. </p>
    <h3>Como acessar a API através do Swagger</h3>
    <p>Após depurar, a tela do Swagger abrirá no navegador monstrando os métodos com acesso para usuário. Porém é necessário a autenticação de um usuário antes de poder acessar os métodos de Tarefas. Segue abaixo os passos para autenticar o usuário:
        <ol>
            <li>Acessar o método <b>RegistraUsuario</b> e criar um novo usuário. No método existe um exemplo de como a requisição JSON deve ser enviada.</li>
            <li>Acessar o método <b>login</b> e usar as credênciais crioadas no passo anterior para efetuar o login. No método existe um exemplo de como a requisição JSON deve ser enviada.</li>
            <li>Caso as credênciais estejam corretas, o sistema reponderá com os dados de acesso. deve-se então copiar o conteúdo do parâmetro <b>token</b> sem as aspas.</li>
            <li>Com o token em mãos deve-se clicar no botão <b>Authorize</b> na parte superior da tela, no campo Value inserir a palavra "Bearer" [espaço] e colar seu token de acesso. </li>
            <li>Então clicar em Authorize e close quando a autorização for confirmada.</li>
        </ol>
    </p> 
    <h3>Métodos Acessiveis pelo Swagger</h3>
    <ul>
        <li>RegistraUsuario: Registra um novo usuário no sistema.</li>
        <ul>
            <li>Exemplo de requisição: <br> 
                { <br>
                      "email": "usuario@teste.com",<br>
                      "password": "@Ab2cd",<br>
                      "confirmPassword": "@Ab2cd"  <br>
                }
            </li>
        </ul><br>
        <li>login: Registra acesso do usuário no sistema, liberando o token de autorização para acessar os métodos do controller Tarefas.</li>
        <ul>
            <li>Exemplo de requisição: <br> 
                { <br>
                      "email": "usuario@teste.com",<br>
                      "password": "@Ab2cd",<br>
                      "confirmPassword": "@Ab2cd"  <br>
                }
            </li>
        </ul><br>
        <li>ObterTodasAsTarefas: Retorna uma lista contendo os dados de todas as tarefas.</li><br>
        <li>ObterTarefaPorId: Retorna os dados de uma tarefa após informar seu Id</li><br>
        <li>CriarTarefa: Inclui uma nova tarefa no banco de dados.</li><br>
        <ul>
            <li>Exemplo de requisição: <br> 
                { <br>
                      "email": "usuario@teste.com",<br>
                      "password": "@Ab2cd",<br>
                      "confirmPassword": "@Ab2cd",  <br>
                      "dataCriacao": "2023-10-31T00:22:25.939Z",<br>
                      "status": 0<br>
                }
            </li>
        </ul><br>
        <li>AlterarTarefa: Atualiza uma tarefa já existente.</li>
        <ul>
            <li>Exemplo de requisição: <br> 
                { <br>
                      "tarefaId": 4 <br>
                      "email": "usuario@teste.com",<br>
                      "password": "@Ab2cd",<br>
                      "confirmPassword": "@Ab2cd"  <br>,  
                      "dataCriacao": "2023-10-31T00:22:25.939Z",<br>
                      "status": 0<br>
                }
            </li>
        </ul><br>
        <li>ExcluirTarefa: Exclui uma tarefa do banco de dados após informar seu Id.</li><br>
        <li>ObterTarefasporStatus: Exibe uma lista de tarefas baseada no seu status.</li><br>
        <ul>
            <li>Possíveis valores: <br> 
                      0 - Pendente <br>
                      1 - Em Andamento<br>
                      2 - Concluída<br>
            </li>
        </ul><br>
    </ul>
    <h3>Testes Unitários</h3>
    <p>Os testes de unidade foram criados em um projeto separado chamado <b>Tarefas.API.Testes</b>. Caso o projeto esteja sem a dependência do projeto principal, deve-se seguir os passos abaixo:</p>
    <ul>
        <li>No gerenciador de Soluções, expandir Tarefas.API.Testes e clicar com o botão direito do mouse em dependências.</li>
        <li>Clicar em Adicionar Referência ao Projeto.</li>
        <li>Selecionar Tarefas.API e clicar em OK.</li>
    </ul>
</body>