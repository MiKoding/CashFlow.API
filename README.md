## SOBRE O PROJETO
Esta API, desenvolvida em .NET 8 com os princípios do **Domain-Driven Design (DDD)**, facilita o gerenciamento de despesas pessoais, permitindo o registro de informações detalhadas, como título, data, descrição, valor e tipo de pagamento, com armazenamento seguro em um banco **MySQL**.

Baseada na arquitetura REST, utiliza métodos HTTP padrão e inclui documentação **Swagger** para explorar e testar endpoints. Ferramentas como **AutoMapper**, **FluentAssertions**, **FluentValidation** e **EntityFramework** são empregadas para mapeamento de objetos, testes legíveis, validações intuitivas e simplificação de interações com o banco de dados, garantindo um código limpo e eficiente.


## FEATURES
- Testes de Unidade: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a
qualidade.

- Geração de Relatórios: Capacidade de exportar relatórios detalhados para PDF e Excel,
oferecendo uma análise visual e eficaz das despesas.

- RESTful API com Documentação Swagger: Interface documentada que facilita a integração e o
teste por parte dos desenvolvedores.

## REQUISITOS
- Visual Studio 2022+ ou Visual Studio Code
- MySql

## INSTALAÇÃO
- Clone o repositório:
  ```sh
  https://github.com/MiKoding/CashFlow.API.git
  ```
  - Preencha as informações do `appsettings.Development.json`
  - Pode testar :)
